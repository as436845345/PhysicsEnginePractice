using SkiaSharp;

namespace PhysicsEngine.Shapes;

public abstract class Shape(IList<PEVector> vectors)
{
    public IList<PEVector> Vectors { get; } = vectors;

    public abstract void Draw(SKCanvas canvas);

    public virtual void Move(PEVector v)
    {
        foreach (PEVector vector in Vectors)
        {
            vector.Add(v);
        }
    }
}
