using SkiaSharp;

namespace PhysicsEngine.Shapes;

public sealed class LineSegment : Shape
{
    public LineSegment(IList<PEVector> vectors) : base(vectors)
    {
        ArgumentNullException.ThrowIfNull(nameof(vectors));

        if (vectors.Count < 2)
            throw new ArgumentOutOfRangeException(nameof(vectors));

        StartPos = vectors[0];
        EndPos = vectors[1];
    }

    public PEVector StartPos { get; }
    public PEVector EndPos { get; }
    public PEVector Center => PEVector.Scale(StartPos + EndPos, 0.5f);

    public bool ShowPoint { get; set; }
    public bool ShowArrow { get; set; }
    public float PointRadius { get; set; } = 2f;

    public override void Draw(SKCanvas canvas)
    {
        const int Gap = 10;

        using var linePaint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1 };

        // 绘制线段
        canvas.DrawLine(StartPos.ToSKPoint(), EndPos.ToSKPoint(), linePaint);

        if (ShowPoint)
        {
            canvas.DrawCircle(StartPos.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Red });
            canvas.DrawCircle(EndPos.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Red });
            canvas.DrawCircle(Center.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Red });
        }

        if (ShowArrow)
        {
            // 向量减法 A−B 的结果是一个从 B 指向 A 的向量（从起点指向终点的方向向量）
            // startPos -> endPos
            var direction = EndPos - StartPos;

            // 归一化得到单位方向向量
            direction.Normalize();

            /* startPos + n*direction = endPos */

            var endHeadCenter = EndPos - PEVector.Scale(direction, Gap);

            // 获取当前向量的法线向量（或称为法向量、垂直向量）
            // 对于一个二维向量(x,y)，它的法线向量通常有两个：(−y,x) 和 (y,−x)
            // 这里是 (y,−x)，
            var directionToLeft = direction.GetNormal();

            // 向左移动
            var leftEndHeadPos = endHeadCenter + PEVector.Scale(directionToLeft, Gap);
            canvas.DrawLine(leftEndHeadPos.ToSKPoint(), EndPos.ToSKPoint(), linePaint);

            // 这里是 (−y,x)
            var directionToRight = PEVector.Scale(direction.GetNormal(), -1);

            // 向右移动
            var rightEndHeadPos = endHeadCenter + PEVector.Scale(directionToRight, Gap);
            canvas.DrawLine(rightEndHeadPos.ToSKPoint(), EndPos.ToSKPoint(), linePaint);

            if (ShowPoint)
            {
                canvas.DrawCircle(endHeadCenter.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Red });
                canvas.DrawCircle(leftEndHeadPos.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Green });
                canvas.DrawCircle(rightEndHeadPos.ToSKPoint(), PointRadius, new SKPaint { Color = SKColors.Green });
            }
        }
    }
}
