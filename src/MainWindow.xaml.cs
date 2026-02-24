using PhysicsEngine.Shapes;
using System.Windows;

namespace PhysicsEngine;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IList<Shape> _shapes = [];

    public MainWindow()
    {
        InitializeComponent();

        var lineSegment = new LineSegment([new PEVector(200, 600), new PEVector(600, 200)])
        {
            ShowArrow = true,
            //ShowPoint = true,
        };
        _shapes.Add(lineSegment);

        var circle = new Circle([new PEVector(300, 300)], 50);
        _shapes.Add(circle);
    }

    private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        foreach (var shape in _shapes)
        {
            shape.Draw(canvas);
        }
    }
}