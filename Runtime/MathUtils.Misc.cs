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

        // Lower triangular matrix, row-wise
        // based on: https://hal.archives-ouvertes.fr/hal-02047514/document
        public static int2 ConvertFromTriMatId(int index)
        {
            var p = (math.sqrt(1 + 8 * (index + 1)) - 1) / 2;
            var j0 = (int)math.floor(p);
            return p == j0 ? j0 - 1 : new int2(index - j0 * (j0 + 1) / 2, j0);
        }

        public static int ConvertToTriMatId(int i, int j) => i + j * (j + 1) / 2;

        /// <summary>
        /// Utility function for id enumeration in bilateral interleaving order, 
        /// e.g. sequence of <paramref name="id"/>=0, 1, 2, 3, 4, 5 (<paramref name="count"/>=6),
        /// will be enumerated in the following way: 0, 5, 1, 4, 2, 3.
        /// </summary>
        public static int BilateralInterleavingId(int id, int count) => (id & 1) == 0 ? id >> 1 : count - 1 - (id >> 1);

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