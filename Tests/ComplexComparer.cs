using System.Collections.Generic;
using UnityEngine.TestTools.Utils;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class ComplexComparer : IEqualityComparer<Complex>
    {
        private const float DefaultEpsilon = 0.0001f;
        private readonly float epsilon;

        public static readonly ComplexComparer Instance = new(DefaultEpsilon);
        public ComplexComparer(float epsilon) => this.epsilon = epsilon;

        public bool Equals(Complex expected, Complex actual)
        {
            return true
                && Utils.AreFloatsEqual(expected.Im, actual.Im, epsilon)
                && Utils.AreFloatsEqual(expected.Re, actual.Re, epsilon)
            ;
        }

        public int GetHashCode(Complex _) => 0;
    }
}