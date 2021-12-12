# BurstMathUtils

Burst compatible miscellaneous math related utility functions.

Currently, package focuses on 2d related utlilities, but functions for 3d math will be added in the future as well.

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
    - [float2 Rotate90CCW(this float2 a)](#float2-rotate90ccwthis-float2-a)
    - [float2 Rotate90CW(this float2 a)](#float2-rotate90cwthis-float2-a)
    - [float2x2 ToDiag(this float2 a)](#float2x2-todiagthis-float2-a)
    - [float2x2 Transform(this float2x2 M, float2x2 A)](#float2x2-transformthis-float2x2-m-float2x2-a)
  - [Primitives](#primitives)
    - [(float2 p, float r) TriangleBoundingCircle(float2 a, float2 b, float2 c)](#float2-p-float-r-triangleboundingcirclefloat2-a-float2-b-float2-c)
    - [(float2 p, float r) TriangleCircumcenter(float2 a, float2 b, float2 c)](#float2-p-float-r-trianglecircumcenterfloat2-a-float2-b-float2-c)
    - [float TriangleSignedArea(float2 a, float2 b, float2 c)](#float-trianglesignedareafloat2-a-float2-b-float2-c)
    - [float TriangleSignedArea2(float2 a, float2 b, float2 c)](#float-trianglesignedarea2float2-a-float2-b-float2-c)
  - [Geometry](#geometry)
    - [void PointClosestPointOnLineSegment(float2 a, float2 b0, float2 b1, out float2 p)](#void-pointclosestpointonlinesegmentfloat2-a-float2-b0-float2-b1-out-float2-p)
    - [bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c)](#bool-pointinsidetrianglefloat2-p-float2-a-float2-b-float2-c)
    - [float PointLineSignedDistance(float2 p, float2 n, float2 a)](#float-pointlinesigneddistancefloat2-p-float2-n-float2-a)
    - [void ShortestLineSegmentBetweenLineSegments(float2 a0, float2 a1, float2 b0, float2 b1, out float2 pA, out float2 pB)](#void-shortestlinesegmentbetweenlinesegmentsfloat2-a0-float2-a1-float2-b0-float2-b1-out-float2-pa-out-float2-pb)
  - [Misc](#misc)
    - [int BilateralInterleavingId(int id, int count)](#int-bilateralinterleavingidint-id-int-count)
  - [Dependencies](#dependencies)
  - [TODO](#todo)
  - [Contributors](#contributors)

## Getting started

To use the package choose one of the following:

- Clone or download this repository and then select `package.json` using Package Manager (`Window/Package Manager`).

- Use package manager via git install: `https://github.com/andywiecko/BurstMathUtils/`.

## Features

Package contains single static class with utilities (and extensions), i.e. `MathUtils` which includes many
useful functions related to the following categories
[Algebra](#algebra),
[Primitives](#primitives),
[Geometry](#geometry), and
[Misc](#misc).

## Algebra

### float Angle(float2 a, float2 b)

Angle (in radias) between vectors _a_ and _b_.

### float Cross(float2 a, float2 b)

Two-dimensional cross product between vectors _a_ and _b_.

### void EigenDecomposition(float2x2 matrix, out float2 eigval, out float2x2 eigvec)

Procedures solves eigen problem for symmetric _matrix_.

### float2 Max(float2 a, float2 b, float2 c)

Componentwise maximum of three vectors.

### float2 Min(float2 a, float2 b, float2 c)

Componentwise minimum of three vectors.

### float2x2 OuterProduct(float2 a, float2 b)

Outer product of two vectors, i.e. _a · bᵀ_.

### float2 Rotate90CCW(this float2 a)

Rotated vector _a_ by 90° (counter-clockwise).

### float2 Rotate90CW(this float2 a)

Rotated vector _a_ by 90° (clockwise).

### float2x2 ToDiag(this float2 a)

Diagonal matrix with values _a_ placed in the diagonal.

### float2x2 Transform(this float2x2 M, float2x2 A)

Transformed matrix _M_, with given transformation _A_, i.e. _A · M · Aᵀ_.

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

### void PointClosestPointOnLineSegment(float2 a, float2 b0, float2 b1, out float2 p)

Procedure finds the closest point _p_ on the line segment _(b0, b1)_ to point _a_.

### bool PointInsideTriangle(float2 p, float2 a, float2 b, float2 c)

_true_ if p is inside triangle _(a, b, c)_, _false_ otherwise.

### float PointLineSignedDistance(float2 p, float2 n, float2 a)

Point–line signed distance.

### void ShortestLineSegmentBetweenLineSegments(float2 a0, float2 a1, float2 b0, float2 b1, out float2 pA, out float2 pB)

Procedure finds the shortest line segment _(pA, pB)_, between line segment _(a0, a1)_ and _(b0, b1)_

## Misc

### int BilateralInterleavingId(int id, int count)

Utility function for _id_ enumeration in bilateral interleaving order,
e.g. sequence of _id_= 0, 1, 2, 3, 4, 5 (_count_ = 6),
will be enumerated in the following way: 0, 5, 1, 4, 2, 3.

## Dependencies

- [`Unity.Burst`](https://docs.unity3d.com/Packages/com.unity.burst@1.6/manual/index.html)
- [`Unity.Mathematics`](https://docs.unity3d.com/Packages/com.unity.mathematics@1.2/manual/index.html)
- [`Unity.Collections`](https://docs.unity3d.com/Packages/com.unity.collections@1.0/manual/index.html)
- [`Unity.Jobs`](https://docs.unity3d.com/Manual/JobSystem.html)

## TODO

- More functions and utilities.
- CI/CD.

## Contributors

- [Andrzej Więckowski, Ph.D](https://andywiecko.github.io/).
