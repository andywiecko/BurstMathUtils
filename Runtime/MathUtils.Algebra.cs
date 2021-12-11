﻿using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public partial class MathUtils
    {
        /// <returns>
        /// Angle (in radias) between vectors <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static float Angle(float2 a, float2 b) => math.atan2(Cross(a, b), math.dot(a, b));

        /// <returns>
        /// Two-dimensional cross product between vectors <paramref name="a"/> and <paramref name="b"/>.
        /// </returns>
        public static float Cross(float2 a, float2 b) => a.x * b.y - a.y * b.x;

        /// <summary>
        /// Procedures solves eigen problem for symmetric <paramref name="matrix"/>.
        /// </summary>
        /// <param name="matrix">Matrix to solve.</param>
        /// <param name="eigval">Eigenvalues of the <paramref name="matrix"/>.</param>
        /// <param name="eigvec">Eigenvectors of the <paramref name="matrix"/> (columns).</param>
        public static void EigenDecomposition(float2x2 matrix, out float2 eigval, out float2x2 eigvec)
        {
            var a00 = matrix[0][0];
            var a11 = matrix[1][1];
            var a01 = matrix[0][1];

            var a00a11 = a00 - a11;
            var p1 = a00 + a11;
            var p2 = (a00a11 >= 0 ? 1 : -1) * math.sqrt(a00a11 * a00a11 + 4 * a01 * a01);
            var lambda1 = p1 + p2;
            var lambda2 = p1 - p2;
            eigval = 0.5f * math.float2(lambda1, lambda2);

            var phi = 0.5f * math.atan2(2 * a01, a00a11);

            eigvec = math.float2x2
            (
                m00: math.cos(phi), m01: -math.sin(phi),
                m10: math.sin(phi), m11: math.cos(phi)
            );
        }

        /// <returns>
        /// Componentwise maximum of three vectors.
        /// </returns>
        public static float2 Max(float2 a, float2 b, float2 c) => math.max(math.max(a, b), c);

        /// <returns>
        /// Componentwise minimum of three vectors.
        /// </returns>
        public static float2 Min(float2 a, float2 b, float2 c) => math.min(math.min(a, b), c);

        /// <returns>
        /// Outer product of two vectors, i.e. <paramref name="a"/>·<paramref name="b"/>ᵀ.
        /// </returns>
        public static float2x2 OuterProduct(float2 a, float2 b) => math.float2x2(a * b[0], a * b[1]);

        /// <returns>
        /// Rotated vector <paramref name="a"/> by 90° (counter-clockwise).
        /// </returns>
        public static float2 Rotate90CCW(this float2 a) => math.float2(-a.y, a.x);

        /// <returns>
        /// Rotated vector <paramref name="a"/> by 90° (clockwise).
        /// </returns>
        public static float2 Rotate90CW(this float2 a) => math.float2(a.y, -a.x);

        /// <returns>
        /// Diagonal matrix with values <paramref name="a"/> placed in the diagonal.
        /// </returns>
        public static float2x2 ToDiag(this float2 a) => math.float2x2(m00: a.x, m01: 0, m10: 0, m11: a.y);

        /// <param name="M"></param>
        /// <param name="A"></param>
        /// <param name="inverse"></param>
        /// <returns>
        /// Transformed matrix <paramref name="M"/>, with given transformation <paramref name="A"/>,
        /// i.e. <paramref name="A"/>·<paramref name="M"/>·<paramref name="A"/>ᵀ.
        /// </returns>
        public static float2x2 Transform(this float2x2 M, float2x2 A) => math.mul(A, math.mul(M, math.transpose(A)));
    }
}