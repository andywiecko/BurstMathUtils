using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class MathUtilsMiscEditorTests
    {
        [Test]
        public void ConvertFromTriMatLIdTest()
        {
            //       0   1   2   3
            //     ______________
            // 0  | a0
            // 1  | a1  a2
            // 2  | a3  a4  a5
            // 3  | a6  a7  a8  a9
            //
            var result = new List<int>();
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i <= j; i++)
                {
                    result.Add(MathUtils.ConvertToTriMatId(i, j));
                }
            }

            Assert.That(result, Is.EqualTo(Enumerable.Range(start: 0, count: 10)));
        }

        [Test]
        public void ConvertToTriMatIdTest()
        {
            //       0   1   2   3
            //     ______________
            // 0  | a0
            // 1  | a1  a2
            // 2  | a3  a4  a5
            // 3  | a6  a7  a8  a9
            //
            var result = new List<int2>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(MathUtils.ConvertFromTriMatId(i));
            }

            int2[] expected = 
            {
                new(0, 0),
                new(0, 1), new(1, 1),
                new (0, 2), new(1, 2), new(2, 2),
                new(0, 3), new(1, 3), new(2, 3), new(3, 3)
            };
            Assert.That(result, Is.EqualTo(expected));
        }

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