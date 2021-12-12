using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Bounding circle at point <em>p</em> and radius <em>r</em> of the triangle 
        /// (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// </returns>
        public static (float2 p, float r) TriangleBoundingCircle(float2 a, float2 b, float2 c)
        {
            var ab = b - a;
            var bc = c - b;
            var ca = a - c;

            var angles = math.abs(math.float3
            (
                Angle(ab, -ca),
                Angle(bc, -ab),
                Angle(ca, -bc)
            ));

            if (angles.x >= math.radians(90))
            {
                return (0.5f * (b + c), 0.5f * math.distance(b, c));
            }
            if (angles.y >= math.radians(90))
            {
                return (0.5f * (a + c), 0.5f * math.distance(a, c));
            }
            if (angles.z >= math.radians(90))
            {
                return (0.5f * (b + a), 0.5f * math.distance(b, a));
            }

            return TriangleCircumcenter(a, b, c);
        }

        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Circumcenter at point <em>p</em> and radius <em>r</em> of the triangle 
        /// (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// </returns>
        public static (float2 p, float r) TriangleCircumcenter(float2 a, float2 b, float2 c)
        {
            var aLenSq = math.lengthsq(a);
            var bLenSq = math.lengthsq(b);
            var cLenSq = math.lengthsq(c);

            var d = 2 * (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y));
            var p = math.float2
            (
                x: aLenSq * (b.y - c.y) + bLenSq * (c.y - a.y) + cLenSq * (a.y - b.y),
                y: aLenSq * (c.x - b.x) + bLenSq * (a.x - c.x) + cLenSq * (b.x - a.x)
            ) / d;

            var r = math.distance(p, a);
            return (p, r);
        }

        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Signed area of triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// </returns>
        public static float TriangleSignedArea(float2 a, float2 b, float2 c) => 0.5f * TriangleSignedArea2(a, b, c);

        /// <param name="a">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="b">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <param name="c">Triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>) vertex position.</param>
        /// <returns>
        /// Doubled signed area of triangle (<paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>).
        /// </returns>
        public static float TriangleSignedArea2(float2 a, float2 b, float2 c) => Cross(b - a, c - a);
    }
}