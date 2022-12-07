# BurstMathUtils

[![Editor tests](https://github.com/andywiecko/BurstMathUtils/actions/workflows/test.yml/badge.svg)](https://github.com/andywiecko/BurstMathUtils/actions/workflows/test.yml)
[![openupm](https://img.shields.io/npm/v/com.andywiecko.burst.mathutils?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.andywiecko.burst.mathutils/)

Burst compatible miscellaneous math related utility functions.

Currently, the package focuses on 2d related utilities, but functions for 3d math will be added as well.

## Table od Contents

- [BurstMathUtils](#burstmathutils)
  - [Table od Contents](#table-od-contents)
  - [Getting started](#getting-started)
  - [Features](#features)
  - [Algebra](#algebra)
    - [float Angle(float2 a, float2 b)](#float-anglefloat2-a-float2-b)
    - [float Cross(float2 a, float2 b)](#float-crossfloat2-a-float2-b)
    - [void EigenDecomposition(float2x2 matrix, out float2 eigval, out float2x2 eigvec)](#void-eigendecompositionfloat2x2-matrix-out-float2-eigval-out-float2x2-eigvec)
    - [float2 Max(float2 a, float2 b, float2 c)](#float2-maxfloat2-a-float2-b-float2-c)
    - [float2 Min(float2 a, float2 b, float2 c)](#float2-minfloat2-a-float2-b-float2-c)
    - [float2x2 OuterProduct(float2 a, float2 b)](#float2x2-outerproductfloat2-a-float2-b)
    - [void PolarDecomposition(float2x2 A, out float2x2 U)](#void-polardecompositionfloat2x2-a-out-float2x2-u)
    - [void PolarDecomposition(float2x2 A, out float2x2 U, out float2x2 P)](#void-polardecompositionfloat2x2-a-out-float2x2-u-out-float2x2-p)
    - [float2 Right()](#float2-right)
    - [float2 Rotate90CCW(this float2 a)](#float2-rotate90ccwthis-float2-a)
    - [float2 Rotate90CW(this float2 a)](#float2-rotate90cwthis-float2-a)
    - [float2x2 ToDiag(this float2 a)](#float2x2-todiagthis-float2-a)
    - [float2x2 Transform(this float2x2 M, float2x2 A)](#float2x2-transformthis-float2x2-m-float2x2-a)
    - [float2 Up()](#float2-up)
  - [Primitives](#primitives)
    - [(float2 p, float r) TriangleBoundingCircle(float2 a, float2 b, float2 c)](#float2-p-float-r-triangleboundingcirclefloat2-a-float2-b-float2-c)
    - [(float2 p, float r) TriangleCircumcenter(float2 a, float2 b, float2 c)](#float2-p-float-r-trianglecircumcenterfloat2-a-float2-b-float2-c)
    - [float TriangleSignedArea(float2 a, float2 b, float2 c)](#float-trianglesignedareafloat2-a-float2-b-float2-c)
    - [float TriangleSignedArea2(float2 a, float2 b, float2 c)](#float-trianglesignedarea2float2-a-float2-b-float2-c)
  - [Geometry](#geometry)
    - [float2 Barycentric(float2 a, float2 b, float2 p)](#float2-barycentricfloat2-a-float2-b-float2-p)
    - [float3 Barycentric(float2 a, float2 b, float2 c, float2 p)](#float3-barycentricfloat2-a-float2-b-float2-c-float2-p)
    - [float2 BarycentricSafe(float2 a, float2 b, float2 p, float2 @default = default)](#float2-barycentricsafefloat2-a-float2-b-float2-p-float2-default--default)
    - [float3 BarycentricSafe(float2 a, float2 b, float2 c, float2 p, float3 @default = default)](#float3-barycentricsafefloat2-a-float2-b-float2-c-float2-p-float3-default--default)
    - [float CCW(float2 a, float2 b, float2 c)](#float-ccwfloat2-a-float2-b-float2-c)
    - [bool IsConvexQuadrilateral(float2 a, float2 b, float2 c, float2 d)](#bool-isconvexquadrilateralfloat2-a-float2-b-float2-c-float2-d)
    - [void PointClosestPointOnLineSegment(float2 a, float2 b0, float2 b1, out float2 p)](#void-pointclosestpointonlinesegmentfloat2-a-float2-b0-float2-b1-out-float2-p)
    - [bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c)](#bool-pointinsidetrianglefloat2-p-float2-a-float2-b-float2-c)
    - [bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c, out float3 bar)](#bool-pointinsidetrianglefloat2-p-float2-a-float2-b-float2-c-out-float3-bar)
    - [float PointLineSignedDistance(float2 p, float2 n, float2 a)](#float-pointlinesigneddistancefloat2-p-float2-n-float2-a)
    - [void ShortestLineSegmentBetweenLineSegments(float2 a0, float2 a1, float2 b0, float2 b1, out float2 pA, out float2 pB)](#void-shortestlinesegmentbetweenlinesegmentsfloat2-a0-float2-a1-float2-b0-float2-b1-out-float2-pa-out-float2-pb)
  - [Misc](#misc)
    - [int BilateralInterleavingId(int id, int count)](#int-bilateralinterleavingidint-id-int-count)
    - [int2 ConvertFromTriMatId(int index)](#int2-convertfromtrimatidint-index)
    - [int ConvertToTriMatId(int i, int j)](#int-converttotrimatidint-i-int-j)
    - [float Round(float value, int digits)](#float-roundfloat-value-int-digits)
    - [float2 Round(float value, int digits)](#float2-roundfloat-value-int-digits)
    - [float3 Round(float value, int digits)](#float3-roundfloat-value-int-digits)
  - [Complex](#complex)
    - [Complex Conjugate(Complex z)](#complex-conjugatecomplex-z)
    - [Complex LookRotation(float2 direction)](#complex-lookrotationfloat2-direction)
    - [Complex LookRotationSafe(float2 direction, float2 @default = default)](#complex-lookrotationsafefloat2-direction-float2-default--default)
    - [Complex Normalize(Complex z)](#complex-normalizecomplex-z)
    - [Complex NormalizeSafe(Complex z, Complex @default = default)](#complex-normalizesafecomplex-z-complex-default--default)
    - [Complex Polar(float r, float phi)](#complex-polarfloat-r-float-phi)
    - [Complex PolarUnit(float phi)](#complex-polarunitfloat-phi)
    - [Complex Pow(Complex z, float x)](#complex-powcomplex-z-float-x)
    - [Complex Reciprocal(Complex z)](#complex-reciprocalcomplex-z)
  - [Dependencies](#dependencies)

## Getting started

Install the package using one of the following methods

<details open>
<summary> Using scoped registry <b>(recommended)</b> </summary>
Use OpenUPM CLI or add corresponding entries to the project's <code>manifest.json</code> manually.
Add or modify scoped registries in the manifest
<pre>
  "scopedRegistries": [
    {
      "name": "OpenUPM",
      "url": "https://package.openupm.com/",
      "scopes": [
        "com.andywiecko"
      ]
    }
  ]
</pre>
and in the dependencies provide selected version of the package
<pre>
"dependencies": {
    "com.andywiecko.burst.mathutils": "1.3.1",
    ...
</pre>
See Unity docs for more details https://docs.unity3d.com/2021.1/Documentation/Manual/upm-scoped.html
</details>

<details>
<summary> <code>git</code> install </summary>
Use package manager via git install: https://github.com/andywiecko/BurstMathUtils.git#v1.3.1
</details>

<details>
<summary> Manual instalation </summary>
Clone or download this repository and then select <code>package.json</code> using Package Manager (<code>Window/Package Manager</code>).
</details>

## Features

Package contains a static class with utilities (and extensions), i.e. `MathUtils` which includes many
useful functions related to the following categories
[Algebra](#algebra),
[Primitives](#primitives),
[Geometry](#geometry), and
[Misc](#misc).

Additionally, the package introduces struct [Complex](#complex) struct, a burst-friendly complex number representation.

## Algebra

### float Angle(float2 a, float2 b)

Angle (in radias) between vectors _a_ and _b_.

### float Cross(float2 a, float2 b)

Two-dimensional cross product between vectors _a_ and _b_.

### void EigenDecomposition(float2x2 matrix, out float2 eigval, out float2x2 eigvec)

Procedure solves eigen problem for symmetric _matrix_.

### float2 Max(float2 a, float2 b, float2 c)

Componentwise maximum of three vectors.

### float2 Min(float2 a, float2 b, float2 c)

Componentwise minimum of three vectors.

### float2x2 OuterProduct(float2 a, float2 b)

Outer product of two vectors, i.e. _a · bᵀ_.

### void PolarDecomposition(float2x2 A, out float2x2 U)

Procedure solves polar decomposition problem for matrix _A_,
formulated as _A_ = _U_ · _P_, where _U_ a is unitary matrix
and _P_ is a positive semi-definite Hermitian matrix.

### void PolarDecomposition(float2x2 A, out float2x2 U, out float2x2 P)

Procedure solves polar decomposition problem for matrix _A_,
formulated as _A_ = _U_ · _P_, where _U_ a is unitary matrix
and _P_ is a positive semi-definite Hermitian matrix.

### float2 Right()

Right axis (1, 0).

### float2 Rotate90CCW(this float2 a)

Rotated vector _a_ by 90° (counter-clockwise).

### float2 Rotate90CW(this float2 a)

Rotated vector _a_ by 90° (clockwise).

### float2x2 ToDiag(this float2 a)

Diagonal matrix with values _a_ placed in the diagonal.

### float2x2 Transform(this float2x2 M, float2x2 A)

Transformed matrix _M_, with given transformation _A_, i.e. _A · M · Aᵀ_.

### float2 Up()

Up axis (0, 1).

## Primitives

### (float2 p, float r) TriangleBoundingCircle(float2 a, float2 b, float2 c)

Bounding circle at point _p_ and radius _r_ of the triangle _(a, b, c)_

### (float2 p, float r) TriangleCircumcenter(float2 a, float2 b, float2 c)

Circumcenter at point _p_ and radius _r_ of the triangle _(a, b, c)_

### float TriangleSignedArea(float2 a, float2 b, float2 c)

Signed area of triangle _(a, b, c)_.

### float TriangleSignedArea2(float2 a, float2 b, float2 c)

Doubled signed area of triangle  _(a, b, c)_.

## Geometry

### float2 Barycentric(float2 a, float2 b, float2 p)

Position _p_ expressed in barycentric coordinate system defined within line segment _(a, b)_.

### float3 Barycentric(float2 a, float2 b, float2 c, float2 p)

Position _p_ expressed in barycentric coordinate system defined within triangle _(a, b, c)_.

### float2 BarycentricSafe(float2 a, float2 b, float2 p, float2 @default = default)

Position _p_ expressed in barycentric coordinate system defined within line segment _(a, b)_.
If the result is not finite, then returns _default_.

### float3 BarycentricSafe(float2 a, float2 b, float2 c, float2 p, float3 @default = default)

Position _p_ expressed in barycentric coordinate system defined within triangle _(a, b, c)_.
If the result is not finite, then returns _default_.

### float CCW(float2 a, float2 b, float2 c)

Triangle _(a, b, c)_ counterclockwise check.

Returns:

- **+1** triangle is counterclockwise.
- **0** triangle is degenerate (colinear points).
- **-1** triangle is clockwise.

### bool IsConvexQuadrilateral(float2 a, float2 b, float2 c, float2 d)

_true_ if quadrilateral _(a, b, c, d)_ is convex, _false_ otherwise.

### void PointClosestPointOnLineSegment(float2 a, float2 b0, float2 b1, out float2 p)

Procedure finds the closest point _p_ on the line segment _(b0, b1)_ to point _a_.

### bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c)

_true_ if _p_ is inside triangle _(a, b, c)_, _false_ otherwise.

### bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c, out float3 bar)

_true_ if _p_ is inside triangle _(a, b, c)_, _false_ otherwise.
_bar_ corresponds to barycentric coordinates of _p_ in triangle _(a, b, c)_.

### float PointLineSignedDistance(float2 p, float2 n, float2 a)

Point–line signed distance.

### void ShortestLineSegmentBetweenLineSegments(float2 a0, float2 a1, float2 b0, float2 b1, out float2 pA, out float2 pB)

Procedure finds the shortest line segment _(pA, pB)_, between line segments _(a0, a1)_ and _(b0, b1)_

## Misc

### int BilateralInterleavingId(int id, int count)

Utility function for _id_ enumeration in bilateral interleaving order,
e.g. sequence of _id_ = 0, 1, 2, 3, 4, 5 (_count_ = 6),
will be enumerated in the following way: 0, 5, 1, 4, 2, 3.

### int2 ConvertFromTriMatId(int index)

Utility function for converting linear index into 2-d index for lower triangular matrix, stored in a row-wise fashion.

Based on the paper[^angeletti.bonny.2019].

### int ConvertToTriMatId(int i, int j)

Utility function for converting 2-d index into linear index for lower triangular matrix, stored in a row-wise fashion.

### float Round(float value, int digits)

Rounds _value_ with respect to given _digits_.

### float2 Round(float value, int digits)

Rounds _value_ with respect to given _digits_.

### float3 Round(float value, int digits)

Rounds _value_ with respect to given _digits_.

## Complex

Struct complex is a burst-friendly complex number representation.
It supports basic arthimetic operators `+, -, *, /`.
Complex struct implementation is based on `Unity.Collections.float2` so all functions/operations should be optimized using Burst SIMD intrinsics.

Small demo how `Complex` can be used is presented below

```csharp
Complex z = (1.2f, 5.7f);
z += 1f; // Expected: z = (2.2f, 5.7f)

z = (0, 1);
z *= z; // Expected: z = (-1, 0)

z = Complex.Polar(r: 2, phi: math.PI); // Returns 2 * exp(i * PI)
```

Additional utility functions for complex numbers can be found below.

### Complex Conjugate(Complex z)

Conjugated number _z_, i.e. _z_*.

### Complex LookRotation(float2 direction)

Complex number which represent view in the given direction.

### Complex LookRotationSafe(float2 direction, float2 @default = default)

Complex number which represent view in the given direction.
If rotation cannot be represented by the finite number, then returns _default_.

### Complex Normalize(Complex z)

Normalized complex number _z_.

### Complex NormalizeSafe(Complex z, Complex @default = default)

Normalized complex number _z_.
If normalized number cannot be represented by the finite number, then returns _default_.

### Complex Polar(float r, float phi)

Complex number via polar construction: _r_ · eⁱᵠ.

### Complex PolarUnit(float phi)

Complex number via (unit length) polar construction: _r_ · eⁱᵠ.

### Complex Pow(Complex z, float x)

_z_ raised to the power _x_, i.e. _zˣ_.

### Complex Reciprocal(Complex z)

Repciprocal of _z_, i.e. _z⁻¹_.

## Dependencies

- [`Unity.Burst`](https://docs.unity3d.com/Packages/com.unity.burst@1.6/manual/index.html)
- [`Unity.Mathematics`](https://docs.unity3d.com/Packages/com.unity.mathematics@1.2/manual/index.html)
- [`Unity.Collections`](https://docs.unity3d.com/Packages/com.unity.collections@1.0/manual/index.html)
- [`Unity.Jobs`](https://docs.unity3d.com/Packages/com.unity.jobs@0.11/manual/index.html)

[^angeletti.bonny.2019]:M. Angeletti, J-M. Bonny, and J. Koko. ["Parallel Euclidean distance matrix computation on big datasets."](https://hal.archives-ouvertes.fr/hal-02047514/document) (2019).
