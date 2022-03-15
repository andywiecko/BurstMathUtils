using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        public static float2 Barycentric(float2 a, float2 b, float2 p)
        {
            var norm2 = math.distancesq(a, b);
            float2 bar;
            bar.x = math.dot(p - b, a - b) / norm2;
            bar.y = 1 - bar.x;
            return bar;
        }

        public static float2 BarycentricSafe(float2 a, float2 b, float2 p, float2 @default = default)
        {
            return math.distancesq(a, b) <= math.EPSILON ? @default : Barycentric(a, b, p);
        }

        public static float3 Barycentric(float2 a, float2 b, float2 c, float2 p)
        {
            var (v0, v1, v2) = (b - a, c - a, p - a);
            var denInv = 1 / Cross(v0, v1);
            var v = denInv * Cross(v2, v1);
            var w = denInv * Cross(v0, v2);
            var u = 1.0f - v - w;
            return math.float3(u, v, w);
        }

        public static float3 BarycentricSafe(float2 a, float2 b, float2 c, float2 p, float3 @default = default)
        {
            var (v0, v1) = (b - a, c - a);
            var den = Cross(v0, v1);
            return math.abs(den) <= math.EPSILON ? @default : Barycentric(a, b, c, p);
        }

        /// <summary>
        /// Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) 
        /// counterclockwise check.
        /// </summary>
        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><see langword="+1"/> ? triangle is counterclockwise.</item>
        /// <item><see langword="0"/> ? triangle is degenerate (colinear points).</item>
        /// <item><see langword="-1"/> ? triangle is clockwise.</item>
        /// </list>
        /// </returns>
        public static float CCW(float2 a, float2 b, float2 c) => math.sign(Cross(b - a, c - a));

        /// <param name="a">Quadrilateral (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>) vertex position.</param>
        /// <param name="b">Quadrilateral (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>) vertex position.</param>
        /// <param name="c">Quadrilateral (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>) vertex position.</param>
        /// <param name="d">Quadrilateral (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>) vertex position.</param>
        /// <returns>
        /// <see langword="true"/> if quadrilateral 
        /// (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>, <paramref name="d"/>)
        /// is convex, <see langword="false"/> otherwise.
        /// </returns>
        public static bool IsConvexQuadrilateral(float2 a, float2 b, float2 c, float2 d) =>
            CCW(a, c, b) != 0 && CCW(a, c, d) != 0 && CCW(b, d, a) != 0 && CCW(b, d, c) != 0 && // colinear checks
            CCW(a, c, b) != CCW(a, c, d) && CCW(b, d, a) != CCW(b, d, c); // diagonal intersection checks

        /// <summary>
        /// Procedure finds the closest point <paramref name="p"/> 
        /// on the line segment (<paramref name="b0"/>, <paramref name="b1"/>)
        /// to point <paramref name="a"/>.
        /// </summary>
        /// <param name="a">Arbitrary point.</param>
        /// <param name="b0">Line segment (<paramref name="b0"/>, <paramref name="b1"/>) vertex position.</param>
        /// <param name="b1">Line segment (<paramref name="b0"/>, <paramref name="b1"/>) vertex position.</param>
        /// <param name="p">The result, point on line segment (<paramref name="b0"/>, <paramref name="b1"/>).</param>
        public static void PointClosestPointOnLineSegment(float2 a, float2 b0, float2 b1, out float2 p)
        {
            var b0b1 = b1 - b0;
            var b0a = a - b0;
            var norm = math.lengthsq(b0b1);

            if (norm == 0)
            {
                p = b0;
                return;
            }

            var u = math.dot(b0a, b0b1) / norm;
            u = math.clamp(u, 0, 1);
            p = math.lerp(b0, b1, u);
        }

        /// <param name="p">Point of interest.</param>
        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="p"/> is inside triangle 
        /// (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>), 
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c) =>
            math.cmax(-Barycentric(a, b, c, p)) <= 0;

        /// <summary>
        /// Procedure resolves point–line segment continuous intersection for point <em>p</em> and line segment (<em>a</em>, <em>b</em>).
        /// It solves for the first valid intersection assuming starting positions 
        /// <paramref name="p0"/> and (<paramref name="a0"/>, <paramref name="b0"/>),
        /// and final positions as <paramref name="p1"/> and (<paramref name="a1"/>, <paramref name="b1"/>).
        /// Intersection time <paramref name="t"/> is defined in range [0, 1]
        /// and intersection point <paramref name="s"/> in range [0, 1], where 0 corresponds to <em>a</em> and 1 to <em>b</em>.
        /// </summary>
        /// <remarks>
        /// Assumes constant velocities for all points <em>p</em>, <em>a</em>, and <em>b</em>.
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> if intersection occurs, <see langword="false"/> otherwise.
        /// </returns>
        public static bool PointLineSegmentContinuousIntersection(
            float2 p0, float2 p1,
            float2 a0, float2 a1,
            float2 b0, float2 b1,
            out float t, out float s)
        {
            t = s = default;

            var pd = p1 - p0;
            var ad = a1 - a0;
            var bd = b1 - b0;

            var A = Cross(ad, bd) + Cross(bd, pd) + Cross(pd, ad);
            var B = Cross(a0, bd) + Cross(ad, b0) + Cross(b0, pd) + Cross(bd, p0) + Cross(p0, ad) + Cross(pd, a0);
            var C = Cross(a0, b0) + Cross(b0, p0) + Cross(p0, a0);
            if (A == 0)
            {
                // linear equation
                if (B != 0)
                {
                    t = -C / B;
                    s = S(t);
                    return s >= 0 && s <= 1 && t >= 0 && t <= 1;
                }
                else if (B == 0 && C == 0)
                {
                    // TODO: missing case when point intersecst at start
                    // TODO: missing zero velocity case

                    // All points are collinear in time
                    var t0 = -math.dot(p0 - a0, pd - ad) / math.distancesq(pd, ad);
                    var t1 = -math.dot(p0 - b0, pd - bd) / math.distancesq(pd, bd);

                    var tp = math.float2(t0, t1);
                    var inRange = tp >= 0 & tp <= 1;
                    if (math.all(inRange))
                    {
                        t = math.select(t0, t1, t0 < t1);
                        s = math.select(0, 1, t0 < t1);
                        return true;
                    }
                    else if (inRange[0])
                    {
                        (t, s) = (t0, 0);
                        return true;
                    }
                    else if (inRange[1])
                    {
                        (t, s) = (t1, 1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            var delta = B * B - 4 * A * C;

            if (delta < 0)
            {
                return false;
            }
            else if (delta == 0)
            {
                t = -B / (2 * A);
                s = S(t);
                return true;
            }
            else
            {
                var deltaSqrt = math.sqrt(delta);
                var t0 = (-B - deltaSqrt) / (2 * A);
                var t1 = (-B + deltaSqrt) / (2 * A);
                var tp = math.float2(t0, t1);
                var inRange = tp >= 0 & tp <= 1;
                if (inRange[0])
                {
                    (t, s) = (t0, S(t0));
                    return true;
                }

                if (inRange[1])
                {
                    (t, s) = (t1, S(t1));
                    return true;
                }

                return false;
            }

            float S(float t)
            {
                var x = a0 + ad * t;
                var y = b0 + bd * t;
                var z = p0 + pd * t;
                return math.dot(z - x, y - x) / math.distancesq(x, y);
            }
        }

        /// <param name="p">Point of interest.</param>
        /// <param name="n">Line normal.</param>
        /// <param name="a">Point on the line.</param>
        /// <returns>
        /// Point–line signed distance.
        /// </returns>
        public static float PointLineSignedDistance(float2 p, float2 n, float2 a) => math.dot(p - a, n);

        /// <summary>
        /// Procedure finds the shortest line segment (<paramref name="pA"/>, <paramref name="pB"/>),
        /// between line segments (<paramref name="a0"/>, <paramref name="a1"/>)
        /// and (<paramref name="b0"/>, <paramref name="b1"/>).
        /// </summary>
        public static void ShortestLineSegmentBetweenLineSegments(float2 a0, float2 a1, float2 b0, float2 b1, out float2 pA, out float2 pB)
        {
            var r = b0 - a0;
            var u = a1 - a0;
            var v = b1 - b0;
            var ru = math.dot(r, u);
            var rv = math.dot(r, v);
            var uu = math.dot(u, u);
            var uv = math.dot(u, v);
            var vv = math.dot(v, v);

            var det = uu * vv - uv * uv;
            float s, t;
            if (det < 1e-9 * uu * vv)
            {
                s = math.clamp(ru / uu, 0, 1);
                t = 0;
            }
            else
            {
                s = math.clamp((ru * vv - rv * uv) / det, 0, 1);
                t = math.clamp((ru * uv - rv * uu) / det, 0, 1);
            }
            var S = math.clamp((t * uv + ru) / uu, 0, 1);
            var T = math.clamp((s * uv - rv) / vv, 0, 1);

            pA = a0 + S * u;
            pB = b0 + T * v;
        }
    }
}