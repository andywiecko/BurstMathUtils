using NUnit.Framework;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils.Editor.Tests
{
    public class ComplexEditorTests
    {
        [Test]
        public void ImaginaryUnitSqTest()
        {
            var i = Complex.ImaginaryUnit;
            Assert.That(i * i, Is.EqualTo((Complex)(-1f, 0)));
        }

        [Test]
        public void ComplexOperatorsTest()
        {
            Complex z1 = (3.5f, 8.5f);
            Complex z2 = (1.2f, 2.5f);
            var x = 1f;

            Assert.That(-z1, Is.EqualTo((Complex)(-3.5f, -8.5f)));
            Assert.That(z1 + z2, Is.EqualTo((Complex)(4.7f, 11f)));
            Assert.That(z1 + x, Is.EqualTo((Complex)(4.5f, 8.5f)));
            Assert.That(x + z2, Is.EqualTo((Complex)(2.2f, 2.5f)));
            Assert.That(z1 - z2, Is.EqualTo((Complex)(2.3f, 6f)));
            Assert.That(x - z2, Is.EqualTo((Complex)(-0.2f, -2.5f)).Using(ComplexComparer.Instance));
            Assert.That(z1 - x, Is.EqualTo((Complex)(2.5f, 8.5f)));
            Assert.That(z1 * z2, Is.EqualTo((Complex)(-17.05f, 18.95f)));
            Assert.That((-x) * z2, Is.EqualTo(-z2));
            Assert.That(z1 * (-x), Is.EqualTo(-z1));
            Assert.That(z1 / z2, Is.EqualTo((Complex)(3.309493f, 0.1885566f)).Using(ComplexComparer.Instance));
            Assert.That(x / z2, Is.EqualTo(Complex.Reciprocal(z2)));
            Assert.That(z1 / x, Is.EqualTo(z1));
            Assert.That(z1 == z2, Is.False);
            Assert.That(z1 != z2, Is.True);
            Assert.That((Complex)(0, 1) != (Complex)(0, 0), Is.True);
        }

        [Test]
        public void LookRotationTest()
        {
            var direction = math.float2(1, 2);
            Complex expectedResult = 1 / math.sqrt(5) * direction;
            Assert.That(Complex.LookRotation(direction), Is.EqualTo(expectedResult));
        }
        [Test]
        public void LookRotationSafeTest()
        {
            var direction = math.float2(0, 0);
            Complex @default = (1.23f, 4.56f);
            Assert.That(Complex.LookRotationSafe(direction, @default.Value), Is.EqualTo(@default));
        }

        [Test]
        public void PolarTest()
        {
            Assert.That(Complex.Polar(r: 2, phi: 0), Is.EqualTo((Complex)(2, 0)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Polar(r: 2, phi: math.PI), Is.EqualTo((Complex)(-2, 0)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Polar(r: 2, phi: math.PI / 2), Is.EqualTo((Complex)(0, 2)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Polar(r: 2, phi: math.PI / 4), Is.EqualTo(math.sqrt(2) * (Complex)(1, 1)).Using(ComplexComparer.Instance));
        }

        [Test]
        public void PolarUnitTest()
        {
            Assert.That(Complex.PolarUnit(phi: 0), Is.EqualTo((Complex)(1, 0)).Using(ComplexComparer.Instance));
            Assert.That(Complex.PolarUnit(phi: math.PI), Is.EqualTo((Complex)(-1, 0)).Using(ComplexComparer.Instance));
            Assert.That(Complex.PolarUnit(phi: math.PI / 2), Is.EqualTo((Complex)(0, 1)).Using(ComplexComparer.Instance));
            Assert.That(Complex.PolarUnit(phi: math.PI / 4), Is.EqualTo(math.sqrt(2) / 2 * (Complex)(1, 1)).Using(ComplexComparer.Instance));
        }

        [Test]
        public void NormalizeTest()
        {
            Complex z = (1, math.sqrt(3));
            Assert.That(Complex.Normalize(z), Is.EqualTo((Complex)(0.50f, math.sqrt(3) / 2)));
        }

        [Test]
        public void NormalizeSafeTest()
        {
            Complex z = (1, math.sqrt(3));
            Assert.That(Complex.NormalizeSafe(z), Is.EqualTo((Complex)(0.50f, math.sqrt(3) / 2)));
            Assert.That(Complex.NormalizeSafe((0, 0), (1.23f, 4.56f)), Is.EqualTo((Complex)(1.23f, 4.56f)));
        }

        [Test]
        public void ConjugateTest()
        {
            Complex z = (8.52345f, -9.237565f);
            Assert.That(Complex.Conjugate(z), Is.EqualTo((Complex)(8.52345f, +9.237565f)));
        }

        [Test]
        public void PowTest()
        {
            Complex z = (1, 1);

            Assert.That(Complex.Pow(z, 0), Is.EqualTo((Complex)(1, 0)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Pow(z, 1), Is.EqualTo((Complex)(1, 1)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Pow(z, 2), Is.EqualTo((Complex)(0, 2)).Using(ComplexComparer.Instance));
            Assert.That(Complex.Pow(z, 3), Is.EqualTo((Complex)(-2, 2)).Using(ComplexComparer.Instance));
        }

        [Test]
        public void ReciprocalTest()
        {
            Complex z = (1, 2);
            var expected = (Complex)(1, -2) / 5;
            Assert.That(Complex.Reciprocal(z), Is.EqualTo(expected));
        }
    }
}