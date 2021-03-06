using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.TestTools.Utils;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class Float2Comparer : IEqualityComparer<float2>
    {
        private const float DefaultEpsilon = 0.0001f;
        private readonly float epsilon;

        public static readonly Float2Comparer Instance = new Float2Comparer(DefaultEpsilon);
        public Float2Comparer(float epsilon) => this.epsilon = epsilon;

        public bool Equals(float2 expected, float2 actual)
        {
            return true
                && Utils.AreFloatsEqual(expected.x, actual.x, epsilon)
                && Utils.AreFloatsEqual(expected.y, actual.y, epsilon)
            ;
        }

        public int GetHashCode(float2 _) => 0;
    }
}
