using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        /// <param name="a">Line segment (<paramref name="a"/>, <paramref name="b"/> vertex position.</param>
        /// <param name="b">Line segment (<paramref name="a"/>, <paramref name="b"/> vertex position.</param>
        /// <param name="p">Target position</param>
        /// <returns>
        /// Position <paramref name="p"/> expressed in barycentric coordinate system
        /// defined within line segment (<paramref name="a"/>, <paramref name="b"/>).
        /// </returns>
        public static float2 Barycentric(float2 a, float2 b, float2 p)
        {
            var norm2 = math.distancesq(a, b);
            var t = math.dot(p - b, a - b) / norm2;
            return math.float2(t, 1 - t);
        }

        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Position <paramref name="p"/> expressed in barycentric coordinate system
        /// defined within triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// </returns>
        public static float3 Barycentric(float2 a, float2 b, float2 c, float2 p)
        {
            var (v0, v1, v2) = (b - a, c - a, p - a);
            var denInv = 1 / Cross(v0, v1);
            var v = denInv * Cross(v2, v1);
            var w = denInv * Cross(v0, v2);
            var u = 1.0f - v - w;
            return math.float3(u, v, w);
        }

        /// <param name="a">Line segment (<paramref name="a"/>, <paramref name="b"/> vertex position.</param>
        /// <param name="b">Line segment (<paramref name="a"/>, <paramref name="b"/> vertex position.</param>
        /// <param name="p">Target position</param>
        /// <returns>
        /// Position <paramref name="p"/> expressed in barycentric coordinate system
        /// defined within line segment (<paramref name="a"/>, <paramref name="b"/>).
        /// If the result is not finite, then returns <paramref name="default"/>.
        /// </returns>
        public static float2 BarycentricSafe(float2 a, float2 b, float2 p, float2 @default = default)
        {
            return math.distancesq(a, b) <= math.EPSILON ? @default : Barycentric(a, b, p);
        }

        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Position <paramref name="p"/> expressed in barycentric coordinate system
        /// defined within triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// If the result is not finite, then returns <paramref name="default"/>.
        /// </returns>
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
        public static bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c) => PointInsideTriangle(p, a, b, c, out _);

        /// <param name="p">Point of interest.</param>
        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="bar">Barycentric coordinates of point <paramref name="p"/> using (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) triangle.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="p"/> is inside triangle 
        /// (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>), 
        /// <see langword="false"/> otherwise.
        /// </returns>
        public static bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c, out float3 bar)
        {
            bar = Barycentric(a, b, c, p);
            return math.cmax(-bar) <= 0;
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