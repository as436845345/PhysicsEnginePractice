using SkiaSharp;

namespace PhysicsEngine.Shapes;

public sealed class Circle : Shape
{
    public Circle(IList<PEVector> vectors, float radius) : base(vectors)
    {
        if (vectors.Count < 1)
            throw new ArgumentException(nameof(vectors));

        Center = vectors[0];
        Radius = radius;
    }

    public PEVector Center { get; }
    public float Radius { get; }

    public override void Draw(SKCanvas canvas)
    {
        using var paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1, IsStroke = true };

        canvas.DrawCircle(Center.ToSKPoint(), Radius, paint);
    }
}
