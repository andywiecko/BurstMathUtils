using NUnit.Framework;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class MathUtilsAlgebraEditorTests
    {
        private static readonly TestCaseData[] angleTestData = new[]
        {
            new TestCaseData
            ((
                a: math.float2(1, 0),
                b: math.float2(0, 1)
            ))
            {
                TestName = "Test case 1 - canonical vectors",
                ExpectedResult = math.PI / 2
            },
            new TestCaseData
            ((
                a: math.float2(0, 1),
                b: math.float2(1, 0)
            ))
            {
                TestName = "Test case 2 - canonical vectors (swapped)",
                ExpectedResult = -math.PI / 2
            },
            new TestCaseData
            ((
                a: math.float2(2, 1),
                b: math.float2(-1, 2)
            ))
            {
                TestName = "Test case 3",
                ExpectedResult = math.PI / 2
            },
            new TestCaseData
            ((
                a: math.float2(1, 0),
                b: math.float2(1, 0)
            ))
            {
                TestName = "Test case 4 - zero test",
                ExpectedResult = 0
            },
            new TestCaseData
            ((
                a: math.float2(1, 0),
                b: math.float2(-1, 0)
            ))
            {
                TestName = "Test case 5 - opposite dir",
                ExpectedResult = math.PI
            }
        };

        [Test, TestCaseSource(nameof(angleTestData))]
        public float AngleTest((float2 a, float2 b) vectors) => MathUtils.Angle(vectors.a, vectors.b);

        private static readonly TestCaseData[] eigenDecompositionTestData = new[]
        {
            new TestCaseData
            ((
                matrix: math.float2x2(1, 2, 2, 1),
                eigval: math.float2(3, -1),
                eigvec: math.float2x2(
                    c0: math.normalize(math.float2(1, 1)),
                    c1: math.normalize(math.float2(-1, 1)))
            )) { TestName = "float2x2(1, 2, 2, 1)"},
            new TestCaseData
            ((
                matrix: math.float2x2(1, 0, 0, 1),
                eigval: math.float2(1, 1),
                eigvec: math.float2x2(
                    c0: math.normalize(math.float2(1, 0)),
                    c1: math.normalize(math.float2(0, 1)))
            )) { TestName = "float2x2(1, 0, 0, 1)"},
            new TestCaseData
            ((
                matrix: math.float2x2(1, 2, 2, 3),
                eigval: math.float2(2 - math.sqrt(5), 2 + math.sqrt(5)),
                eigvec: math.float2x2(
                    c0: math.normalize(math.float2(-0.5f * (1 - math.sqrt(5)), 1)),
                    c1: math.normalize(math.float2(-0.5f * (1 + math.sqrt(5)), 1)))
            )) { TestName = "float2x2(1, 2, 3, 1)"}
        };

        [Test, TestCaseSource(nameof(eigenDecompositionTestData))]
        public void EigenDecompositionTest((float2x2 matrix, float2 expectedEigval, float2x2 expectedEigvec) data)
        {
            var (matrix, expectedEigval, expectedEigvec) = data;
            MathUtils.EigenDecomposition(matrix, out var eigval, out var eigvec);

            Assert.That(eigval, Is.EqualTo(expectedEigval));
            Assert.That(eigvec, Is.EqualTo(expectedEigvec).Using(Float2x2Comparer.Instance));
        }
    }
}