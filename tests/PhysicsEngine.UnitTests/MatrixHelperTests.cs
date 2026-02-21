using PhysicsEngine.Helpers;

namespace PhysicsEngine.UnitTests;

public class MatrixHelperTests
{
    [Theory]
    [InlineData(2f, 2f, 3f, 0, -(float.Pi / 4))]
    public void CalculateRotationRadian_ReturnsNegative45DegreesInRadians(float startX, float startY, float endX, float endY, float expected)
    {
        var start = new PEVector(startX, startY);
        var end = new PEVector(endX, endY);
        
        var radian = MatrixHelper.CalculateRotationRadian(start, end);
        Assert.Equal(expected, radian);
    }
}