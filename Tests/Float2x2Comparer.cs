using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.TestTools.Utils;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class Float2x2Comparer : IEqualityComparer<float2x2>
    {
        private const float DefaultEpsilon = 0.0001f;
        private readonly float epsilon;

        public static readonly Float2x2Comparer Instance = new Float2x2Comparer(DefaultEpsilon);
        public Float2x2Comparer(float epsilon) => this.epsilon = epsilon;

        public bool Equals(float2x2 expected, float2x2 actual)
        {
            return true
                && Utils.AreFloatsEqual(expected[0][0], actual[0][0], epsilon)
                && Utils.AreFloatsEqual(expected[1][0], actual[1][0], epsilon)
                && Utils.AreFloatsEqual(expected[0][1], actual[0][1], epsilon)
                && Utils.AreFloatsEqual(expected[1][1], actual[1][1], epsilon)
            ;
        }

        public int GetHashCode(float2x2 _) => 0;
    }
}
