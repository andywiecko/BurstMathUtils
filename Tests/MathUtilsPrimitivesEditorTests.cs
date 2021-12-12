using NUnit.Framework;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class MathUtilsPrimitivesEditorTests
    {
        private static readonly TestCaseData[] triangleCircumcenterTestData = new[]
        {
            new TestCaseData(math.float2(0, 0), math.float2(3, 0), math.float2(0, 4))
            {
                ExpectedResult = (center: math.float2(1.5f, 2), radius: 2.5f),
                TestName = "Test Case 1 - Pytagorean triangle"
            },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2))
            {
                ExpectedResult = (center: math.float2(0.5f, math.sqrt(3) / 6), radius: math.sqrt(3) / 3),
                TestName = "Test Case 2 - Equilateral triangle"
            }
        };

        [Test, TestCaseSource(nameof(triangleCircumcenterTestData))]
        public (float2 p, float r) TriangleCircumcenterTest(float2 a, float2 b, float2 c) => MathUtils.TriangleCircumcenter(a, b, c);
    }
}