using NUnit.Framework;
using System.Collections.Generic;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class MathUtilsMiscEditorTests
    {
        [Test]
        public void BilateralInterleavingIdTest()
        {
            var count = 6;
            var result = new List<int>();
            for (int i = 0; i < count; i++)
            {
                var id = MathUtils.BilateralInterleavingId(i, count);
                result.Add(id);
            }

            int[] expectedResult = { 0, 5, 1, 4, 2, 3 };
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}