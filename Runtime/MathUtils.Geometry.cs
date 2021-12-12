using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
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
        public static bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c)
        {
            var area2inv = 1 / Cross(b - a, c - a);
            var s = area2inv * (a.y * c.x - a.x * c.y + (c.y - a.y) * p.x + (a.x - c.x) * p.y);
            var t = area2inv * (a.x * b.y - a.y * b.x + (a.y - b.y) * p.x + (b.x - a.x) * p.y);
            return s >= 0 && t >= 0 && 1 - s - t >= 0;
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