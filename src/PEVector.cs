using SkiaSharp;

namespace PhysicsEngine;

public struct PEVector
{
    public float X;
    public float Y;

    public PEVector() { }

    public PEVector(float x, float y)
    {
        X = x;
        Y = y;
    }

    public readonly float Length => (float)Math.Sqrt(X * X + Y * Y);

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

    public void Sub(PEVector v)
    {
        X -= v.X;
        Y -= v.Y;
    }

    public PEVector GetNormal()
    {
        return new PEVector { X = Y, Y = -X };
    }

    public static PEVector Add(PEVector v1, PEVector v2)
    {
        return v1 + v2;
    }

    public static PEVector Sub(PEVector v1, PEVector v2)
    {
        return v1 - v2;
    }

    public static PEVector Scale(PEVector v, float scale)
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
        return new SKPoint(v.X, v.Y);
    }
}
