using System;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    public static partial class MathUtils
    {
        private static readonly int[] powOfTen =
        {
            1, 10, 100,
            1_000, 10_000, 100_000,
            1_000_000, 10_000_000, 100_000_000
        };

        /// <summary>
        /// Utility function for id enumeration in bilateral interleaving order, 
        /// e.g. sequence of <paramref name="id"/>=0, 1, 2, 3, 4, 5 (<paramref name="count"/>=6),
        /// will be enumerated in the following way: 0, 5, 1, 4, 2, 3.
        /// </summary>
        public static int BilateralInterleavingId(int id, int count) => (id & 1) == 0 ? id >> 1 : count - 1 - (id >> 1);

        /// <summary>
        /// Utility function for converting linear index into 2-d index
        /// for lower triangular matrix, stored in a row-wise fashion.
        /// </summary>
        /// <param name="index">Lower triangular matrix linear index.</param>
        /// <returns>
        /// Lower triangular matrix 2-d index, 
        /// where <c>int2.x</c> is column and <c>int2.y</c> is row index.
        /// </returns>
        /// <remarks>
        /// Based on M. Angeletti, J-M. Bonny, and J. Koko. 
        /// <see href="https://hal.archives-ouvertes.fr/hal-02047514/document">
        /// "Parallel Euclidean distance matrix computation on big datasets."</see> (2019).
        /// </remarks>
        public static int2 ConvertFromTriMatId(int index)
        {
            var p = (math.sqrt(1 + 8 * (index + 1)) - 1) / 2;
            var j0 = (int)math.floor(p);
            return p == j0 ? j0 - 1 : new int2(index - j0 * (j0 + 1) / 2, j0);
        }

        /// <summary>
        /// Utility function for converting 2-d index into linear index
        /// for lower triangular matrix, stored in a row-wise fashion.
        /// </summary>
        /// <param name="i">Lower triangular matrix <b>column</b> index.</param>
        /// <param name="j">Lower triangular matrix <b>row</b> index.</param>
        /// <returns>Lower triangular matrix 2-d index.</returns>
        public static int ConvertToTriMatId(int i, int j) => i + j * (j + 1) / 2;

        /// <summary>
        /// Rounds <paramref name="value"/> with respect to given <paramref name="digits"/>.
        /// </summary>
        public static float Round(float value, int digits)
        {
            CheckDigits(digits);
            var factor = powOfTen[digits];
            return math.round(value * factor) / factor;
        }

        /// <summary>
        /// Rounds <paramref name="value"/> with respect to given <paramref name="digits"/>.
        /// </summary>
        public static float2 Round(float2 value, int digits)
        {
            CheckDigits(digits);
            var factor = powOfTen[digits];
            return math.round(value * factor) / factor;
        }

        /// <summary>
        /// Rounds <paramref name="value"/> with respect to given <paramref name="digits"/>.
        /// </summary>
        public static float3 Round(float3 value, int digits)
        {
            CheckDigits(digits);
            var factor = powOfTen[digits];
            return math.round(value * factor) / factor;
        }

        #region Diagnostics
        [System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private static void CheckDigits(int digits)
        {
            if (digits < 0 || digits > 9)
            {
                throw new ArgumentException("Digits must be in range [0..9]!");
            }
        }
        #endregion
    }
}