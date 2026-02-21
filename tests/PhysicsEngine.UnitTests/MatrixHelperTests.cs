using PhysicsEngine.Helpers;

namespace PhysicsEngine.UnitTests;

public class MatrixHelperTests
{
    [Theory]
    [InlineData(2, 2, 3, 0, -(float.Pi / 4))]
    public void CalculateRotationRadian_ReturnsNegative45DegreesInRadians(double startX, double startY, double endX, double endY, float expected)
    {
        var start = new PEVector(startX, startY);
        var end = new PEVector(endX, endY);
        
        var radian = MatrixHelper.CalculateRotationRadian(start, end);
        Assert.Equal(expected, radian);
    }
}