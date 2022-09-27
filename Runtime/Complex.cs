using System;
using Unity.Mathematics;

namespace andywiecko.BurstMathUtils
{
    /// <summary>
    /// Burst-friendly <em>complex</em> number, i.e. a + b𝒾, where 𝒾² = -1.
    /// </summary>
    [Serializable]
    public struct Complex : IEquatable<Complex>
    {
        /// <summary>
        /// <em>Realis</em>
        /// </summary>
        public float Re { get => Value.x; set => Value.x = value; }

        /// <summary>
        /// <em>Imaginaria</em>
        /// </summary>
        public float Im { get => Value.y; set => Value.y = value; }

        /// <summary>
        /// |<em>z</em>|
        /// </summary>
        public readonly float Magnitude => math.length(Value);

        /// <summary>
        /// |<em>z</em>|²
        /// </summary>
        public readonly float MagnitudeSq => math.lengthsq(Value);

        /// <summary>
        /// Polar angle φ of the complex number.
        /// </summary>
        public float Arg => math.atan2(Im, Re);

        /// <summary>
        /// 1+0𝒾
        /// </summary>
        public static readonly Complex Identity = new(1, 0);

        /// <summary>
        /// 0+1𝒾
        /// </summary>
        public static readonly Complex ImaginaryUnit = new(0, 1);

        /// <summary>
        /// <see cref="float2"/> representation of the complex number <em>z</em>.
        /// </summary>
        public float2 Value;

        public Complex(float x, float y) { Value.x = x; Value.y = y; }
        public Complex(float2 value) => Value = value;
        public Complex(float2x2 m) => Value = m.c0;

        public static implicit operator Complex((float x, float y) z) => new(z.x, z.y);
        public static implicit operator Complex(float2 v) => new(v);
        public static Complex operator -(Complex z) => -z.Value;
        public static Complex operator +(Complex z1, Complex z2) => z1.Value + z2.Value;
        public static Complex operator +(Complex z, float x) => (z.Re + x, z.Im);
        public static Complex operator +(float x, Complex z) => (z.Re + x, z.Im);
        public static Complex operator -(Complex z1, Complex z2) => z1.Value - z2.Value;
        public static Complex operator -(float x, Complex z) => (x - z.Re, -z.Im);
        public static Complex operator -(Complex z, float x) => (z.Re - x, z.Im);
        public static Complex operator *(Complex z1, Complex z2) => new(z1.Re * z2.Re - z1.Im * z2.Im, z1.Re * z2.Im + z1.Im * z2.Re);
        public static Complex operator *(float c, Complex z) => c * z.Value;
        public static Complex operator *(Complex z, float c) => c * z.Value;
        public static Complex operator /(Complex z1, Complex z2) => z1 * Reciprocal(z2);
        public static Complex operator /(float x, Complex z) => x * Reciprocal(z);
        public static Complex operator /(Complex z, float x) => 1 / x * z;
        public static bool operator ==(Complex z1, Complex z2) => math.all(z1.Value == z2.Value);
        public static bool operator !=(Complex z1, Complex z2) => math.any(z1.Value != z2.Value);
        public bool Equals(Complex z) => math.all(Value == z.Value);
        public override bool Equals(object other) => other is Complex z && Equals(z);
        public override int GetHashCode() => (int)math.hash(Value);
        public override string ToString() => $"{Value.x}+{Value.y}i ({Magnitude}∢{Arg})";

        /// <returns>
        /// Conjugated number <paramref name="z"/>, i.e. <paramref name="z"/>*.
        /// </returns>
        public static Complex Conjugate(Complex z) => (z.Re, -z.Im);

        /// <returns>
        /// Complex number which represent view in the given <paramref name="direction"/>.
        /// </returns>
        public static Complex LookRotation(float2 direction) => new(math.normalize(direction));

        /// <returns>
        /// Complex number which represent view in the given <paramref name="direction"/>.
        /// If rotation cannot be represented by the finite number, then returns <paramref name="default"/>.
        /// </returns>
        public static Complex LookRotationSafe(float2 direction, float2 @default = default) =>
            new(math.normalizesafe(direction, @default));

        /// <returns>
        /// Normalized complex number <paramref name="z"/>.
        /// </returns>
        public static Complex Normalize(Complex z) => math.normalize(z.Value);

        /// <returns>
        /// Normalized complex number <paramref name="z"/>.
        /// If normalized number cannot be represented by the finite number, then returns <paramref name="default"/>.
        /// </returns>
        public static Complex NormalizeSafe(Complex z, Complex @default = default) => math.normalizesafe(z.Value, @default.Value);

        /// <returns>
        /// Complex number via polar construction: <em>r</em>·eⁱᵠ.
        /// </returns>
        public static Complex Polar(float r, float phi) => r * PolarUnit(phi);

        /// <returns>
        /// Complex number via (unit length) polar construction: eⁱᵠ.
        /// </returns>
        public static Complex PolarUnit(float phi)
        {
            math.sincos(phi, out var s, out var c);
            return math.float2(c, s);
        }

        /// <returns>
        /// <paramref name="z"/> raised to the power <paramref name="x"/>.
        /// </returns>
        public static Complex Pow(Complex z, float x) => Polar(r: math.pow(z.Magnitude, x), phi: z.Arg * x);

        /// <returns>
        /// Repciprocal of <paramref name="z"/>, i.e. <paramref name="z"/>⁻¹.
        /// </returns>
        public static Complex Reciprocal(Complex z) => 1f / z.MagnitudeSq * Conjugate(z);
    }
}