using SkiaSharp;

namespace PhysicsEngine;

public struct PEVector
{
    public double X;
    public double Y;

    public PEVector() { }

    public PEVector(double x, double y)
    {
        X = x;
        Y = y;
    }

    public readonly double Length => Math.Sqrt(X * X + Y * Y);

    /// <summary>
    /// 归一化：sqrt(x^2+y^2) ≈ 1
    /// </summary>
    public void Normalize()
    {
        var length = Length;
        X /= length;
        Y /= length;
    }

    public void Add(PEVector v)
    {
        X += v.X;
        Y += v.Y;
    }

    public PEVector GetNormal()
    {
        return new PEVector { X = Y, Y = -X };
    }

    public static PEVector Scale(PEVector v, double scale)
    {
        return new PEVector { X = v.X * scale, Y = v.Y * scale };
    }

    public static PEVector operator +(PEVector v1, PEVector v2)
    {
        return new PEVector { X = v1.X + v2.X, Y = v1.Y + v2.Y };
    }

    public static PEVector operator -(PEVector v1, PEVector v2)
    {
        return new PEVector { X = v1.X - v2.X, Y = v1.Y - v2.Y };
    }
}

public static class PEVectorExtensions
{
    public static SKPoint ToSKPoint(this PEVector v)
    {
        return new SKPoint((float)v.X, (float)v.Y);
    }
}
