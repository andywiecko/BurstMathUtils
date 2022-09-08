using NUnit.Framework;
using System.Linq;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class MathUtilsGeometryEditorTests
    {
        private static readonly TestCaseData[] barycentric2TestData = new[]
        {
            new TestCaseData(math.float2(0, 0), math.float2(10, 0), math.float2(5, 0))
            { ExpectedResult = math.float2(0.5f, 0.5f), TestName = "Case 1" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0, 0))
            { ExpectedResult = math.float2(1, 0), TestName = "Case 2" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(1, 0))
            { ExpectedResult = math.float2(0, 1), TestName = "Case 3" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(-1, 0))
            { ExpectedResult = math.float2(2, -1), TestName = "Case 4" },
            new TestCaseData(math.float2(-1, 0), math.float2(1, 0), math.float2(0, 0))
            { ExpectedResult = math.float2(0.5f, 0.5f), TestName = "Case 5" },
            new TestCaseData(math.float2(1, 0), math.float2(-1, 0), math.float2(0, 0))
            { ExpectedResult = math.float2(0.5f, 0.5f), TestName = "Case 6" },
        };

        [Test, TestCaseSource(nameof(barycentric2TestData))]
        public float2 Barycentric2Test(float2 a, float2 b, float2 p) => MathUtils.Barycentric(a, b, p);

        private static readonly TestCaseData[] barycentric3TestData = new[]
        {
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(0.5f, math.sqrt(3) / 6))
            { ExpectedResult = MathUtils.Round((float3) 1 / 3, 6), TestName = "Case 1 (barycenter)" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(0, 0))
            { ExpectedResult = math.float3(1, 0, 0), TestName = "Case 2 (point a)" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(1, 0))
            { ExpectedResult = math.float3(0, 1, 0), TestName = "Case 3 (point b)" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(0.5f, math.sqrt(3) / 2))
            { ExpectedResult = math.float3(0, 0, 1), TestName = "Case 4 (point c)" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(0.5f, 0))
            { ExpectedResult = math.float3(0.5f, 0.5f, 0), TestName = "Case 5 (center ab)" },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, math.sqrt(3) / 2), math.float2(0.25f, 0.25f))
            { ExpectedResult = MathUtils.Round(math.float3(0.605662f, 0.105662f, 0.288675f), 6), TestName = "Case 6 (arbitrary point)" },
        };

        [Test, TestCaseSource(nameof(barycentric3TestData))]
        public float3 Barycentric3Test(float2 a, float2 b, float2 c, float2 p) => MathUtils.Round(MathUtils.Barycentric(a, b, c, p), 6);

        private static readonly TestCaseData[] shortestLineSegmentBetweenLineSegmentsTestData = new[]
        {
            new TestCaseData
            ((
                a0: math.float2(0, 0),
                a1: math.float2(1, 0),
                b0: math.float2(2, 0),
                b1: math.float2(3, 0)
            ))
            {
                TestName = "Test Case 1 - flat line.",
                ExpectedResult = (pA: math.float2(1, 0), pB: math.float2(2, 0))
            },
            new TestCaseData
            ((
                a0: math.float2(-1, 0),
                a1: math.float2(1, 0),
                b0: math.float2(0, -1),
                b1: math.float2(0, 1)
            ))
            {
                TestName = "Test Case 2 - cross.",
                ExpectedResult = (pA: math.float2(0, 0), pB: math.float2(0, 0))
            },
            new TestCaseData
            ((
                a0: math.float2(-1, 0),
                a1: math.float2(1, 0),
                b0: math.float2(0, 1),
                b1: math.float2(0, 2)
            ))
            {
                TestName = "Test Case 3",
                ExpectedResult = (pA: math.float2(0, 0), pB: math.float2(0, 1))
            },
            new TestCaseData
            ((
                a0: math.float2(0, 0),
                a1: math.float2(1, 2),
                b0: math.float2(1, 1),
                b1: math.float2(2, 1)
            ))
            {
                TestName = "Test Case 4 - complex case",
                ExpectedResult = (pA: math.float2(0.6f, 1.2f), pB: math.float2(1, 1))
            },
            new TestCaseData
            ((
                a0: math.float2(1, 1),
                a1: math.float2(2, 1),
                b0: math.float2(0, 0),
                b1: math.float2(1, 2)
            ))
            {
                TestName = "Test Case 5 - case 4 swapped",
                ExpectedResult = (pA: math.float2(1, 1), pB: math.float2(0.6f, 1.2f))
            },
            new TestCaseData
            ((
                a0: math.float2(1.11f, 1.16f),
                a1: math.float2(2.653f, 1.244f),
                b0: math.float2(0.54f, 0.88f),
                b1: math.float2(1.7f, 2.8f)
            ))
            {
                TestName = "Test Case 6 - very complex case",
                ExpectedResult = (pA: math.float2(1.11f, 1.16f), pB: math.float2(0.816f,  1.337f))
            },
            new TestCaseData
            ((
                a0: math.float2(0, 0),
                a1: math.float2(1, 0),
                b0: math.float2(0, 1),
                b1: math.float2(1, 1)
            ))
            {
                TestName = "Test Case 7 - parallel lines",
                ExpectedResult = (pA: math.float2(0, 0), pB: math.float2(0, 1))
            },
            new TestCaseData
            ((
                a0: math.float2(0, 0),
                a1: math.float2(1, 0),
                b0: math.float2(0, 0),
                b1: math.float2(1, 0)
            ))
            {
                TestName = "Test Case 8 - same lines",
                ExpectedResult = (pA: math.float2(0, 0), pB: math.float2(0, 0))
            },
            new TestCaseData
            ((
                a0: math.float2(0, 0),
                a1: math.float2(0, 0),
                b0: math.float2(0, 0),
                b1: math.float2(0, 0)
            ))
            {
                TestName = "Test Case 8 - point",
                ExpectedResult = (pA: math.float2(0, 0), pB: math.float2(0, 0))
            }
        };

        private static readonly TestCaseData[] CCWTestData = new[]
        {
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(0.5f, 1))
            { ExpectedResult = 1f, TestName = "Case 1 (counter clock wise"},
            new TestCaseData(math.float2(0, 0), math.float2(0.5f, 1), math.float2(1, 0))
            { ExpectedResult = -1f, TestName = "Case 2 (clock wise"},
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(2, 0))
            { ExpectedResult = 0f, TestName = "Case 3 (colinear"}
        }.SelectMany(i =>
        {
            var args = i.OriginalArguments;
            var (a, b, c) = (args[0], args[1], args[2]);
            return new[]
            {
                new TestCaseData(a, b, c) { TestName = i.TestName + ", perm 0)", ExpectedResult = i.ExpectedResult },
                new TestCaseData(c, a, b) { TestName = i.TestName + ", perm 1)", ExpectedResult = i.ExpectedResult },
                new TestCaseData(b, c, a) { TestName = i.TestName + ", perm 2)", ExpectedResult = i.ExpectedResult },
            };
        }).ToArray();

        [Test, TestCaseSource(nameof(CCWTestData))]
        public float CCWTest(float2 a, float2 b, float2 c) => MathUtils.CCW(a, b, c);

        private static readonly TestCaseData[] isConvexQuadrilateralTestData = new[]
        {
            new TestCaseData(math.float2(0, 0), math.float2(4, 0), math.float2(4, 1), math.float2(0, 1))
            { TestName = "Case 1 (convex, rectangle", ExpectedResult = true },
            new TestCaseData(math.float2(0, 0), math.float2(8, 0), math.float2(1, 1), math.float2(0, 8))
            { TestName = "Case 2 (concave, dart", ExpectedResult = false },
            new TestCaseData(math.float2(0, 0), math.float2(2, 1), math.float2(2, 0), math.float2(0, 2))
            { TestName = "Case 3 (reflect", ExpectedResult = false },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(2, 0), math.float2(2, 2))
            { TestName = "Case 4 (colinear 3-point", ExpectedResult = false },
            new TestCaseData(math.float2(0, 0), math.float2(1, 0), math.float2(2, 0), math.float2(3, 0))
            { TestName = "Case 5 (colinear 4-point", ExpectedResult = false },
        }.SelectMany(i =>
        {
            var args = i.OriginalArguments;
            var (a, b, c, d) = (args[0], args[1], args[2], args[3]);
            return new[]
            {
                new TestCaseData(a, b, c, d) { TestName = i.TestName + ", perm 0)", ExpectedResult = i.ExpectedResult },
                new TestCaseData(d, a, b, c) { TestName = i.TestName + ", perm 1)", ExpectedResult = i.ExpectedResult },
                new TestCaseData(c, d, a, b) { TestName = i.TestName + ", perm 2)", ExpectedResult = i.ExpectedResult },
                new TestCaseData(b, c, d, a) { TestName = i.TestName + ", perm 3)", ExpectedResult = i.ExpectedResult },
            };
        }).ToArray();

        [Test, TestCaseSource(nameof(isConvexQuadrilateralTestData))]
        public bool IsConvexQuadrilateralTest(float2 a, float2 b, float2 c, float2 d) => MathUtils.IsConvexQuadrilateral(a, b, c, d);

        [Test, TestCaseSource(nameof(shortestLineSegmentBetweenLineSegmentsTestData))]
        public (float2 pA, float2 pB) ShortestLineSegmentBetweenLineSegmentsTest((float2 a0, float2 a1, float2 b0, float2 b1) points)
        {
            var (a0, a1, b0, b1) = points;
            MathUtils.ShortestLineSegmentBetweenLineSegments(
                a0, a1, b0, b1,
                out var pA, out var pB);
            return (math.round(1_000 * pA) / 1_000, math.round(1_000 * pB) / 1_000);
        }

        private static readonly TestCaseData[] pointClosestPointOnLineSegmentTestData = new[]
        {
            new TestCaseData
            ((
                a: (float2)1,
                b0: (float2)2,
                b1: (float2)3
            ))
            {
                TestName = "Test case 1 - t < 0",
                ExpectedResult = (float2)2
            },
            new TestCaseData
            ((
                a: (float2)4,
                b0: (float2)2,
                b1: (float2)3
            ))
            {
                TestName = "Test case 2 - t > 0",
                ExpectedResult = (float2)3
            },
            new TestCaseData
            ((
                a: (float2)2.5f,
                b0: (float2)2,
                b1: (float2)3
            ))
            {
                TestName = "Test case 3 - t in [0, 1]",
                ExpectedResult = (float2)2.5f
            },
            new TestCaseData
            ((
                a: math.float2(0.3567f, 1),
                b0: math.float2(0, 0.2f),
                b1: math.float2(1, 0.2f)
            ))
            {
                TestName = "Test case 4 - t in [0, 1] (more complex)",
                ExpectedResult = math.float2(0.3567f, 0.2f)
            }
        };

        [Test, TestCaseSource(nameof(pointClosestPointOnLineSegmentTestData))]
        public float2 PointClosestPointOnLineSegmentTest((float2 a, float2 b0, float2 b1) points)
        {
            var (a, b0, b1) = points;
            MathUtils.PointClosestPointOnLineSegment(a, b0, b1, out var p);
            return p;
        }

        private static readonly TestCaseData[] pointInsideTriangleTestData = new[]
        {
            new TestCaseData
            ((
                //
                //        *
                //        | `.
                //        |   `.
                //        |     `.
                //        |       `.
                // *      *---------*
                //  
                p: math.float2(-1, 0),
                a: math.float2(0, 0),
                b: math.float2(2, 0),
                c: math.float2(0, 2)
            )) { TestName = "Test case 1", ExpectedResult = false },
            new TestCaseData
            ((
                //
                //   *
                //   | `.
                //   |   `.
                //   |  *  `.
                //   |       `.
                //   *---------*
                //  
                p: math.float2(1, 1),
                a: math.float2(0, 0),
                b: math.float2(2, 0),
                c: math.float2(0, 2)
            )) { TestName = "Test case 2", ExpectedResult = true }
        };

        [Test, TestCaseSource(nameof(pointInsideTriangleTestData))]
        public bool PointInsideTriangleTest((float2 p, float2 a, float2 b, float2 c) data) => MathUtils.PointInsideTriangle(data.p, data.a, data.b, data.c);
    }
}