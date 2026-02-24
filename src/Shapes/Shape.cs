using SkiaSharp;

namespace PhysicsEngine.Shapes;

public abstract class Shape
{
    protected Shape(IList<PEVector> vectors)
    {
        ArgumentNullException.ThrowIfNull(nameof(vectors));

        Vectors = vectors;
    }

    public IList<PEVector> Vectors { get; }

    public abstract void Draw(SKCanvas canvas);

    public virtual void Move(PEVector v)
    {
        foreach (PEVector vector in Vectors)
        {
            vector.Add(v);
        }
    }
}
