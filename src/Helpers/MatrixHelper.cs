namespace PhysicsEngine.Helpers;

/// <summary>
/// 矩阵运算辅助类，提供二维向量旋转相关的核心计算方法
/// </summary>
public static class MatrixHelper
{
    /// <summary>
    /// 基于二维旋转矩阵公式，将向量绕原点逆时针旋转指定弧度
    /// </summary>
    /// <param name="vector">待旋转的二维向量</param>
    /// <param name="rotationRadian">旋转弧度（正数=逆时针，负数=顺时针）</param>
    /// <returns>旋转后的新向量（原向量不会被修改）</returns>
    /// <remarks>
    /// 二维旋转矩阵公式：
    /// [x'] = [cosθ  -sinθ] [x]
    /// [y'] = [sinθ   cosθ] [y]
    /// 展开后：
    /// x' = x * cosθ - y * sinθ
    /// y' = x * sinθ + y * cosθ
    /// </remarks>
    public static PEVector RotateVector(this PEVector vector, float rotationRadian)
    {
        return new PEVector
        {
            X = vector.X * Math.Cos(rotationRadian) - vector.Y * Math.Sin(rotationRadian),
            Y = vector.X * Math.Sin(rotationRadian) + vector.Y * Math.Cos(rotationRadian),
        };
    }

    /// <summary>
    /// 计算从起始向量旋转到目标向量所需的弧度（绕原点逆时针旋转角度）
    /// </summary>
    /// <param name="startVector">旋转起始向量</param>
    /// <param name="targetVector">旋转目标向量</param>
    /// <returns>旋转弧度，范围 [-π, π]（正数=逆时针旋转，负数=顺时针旋转）</returns>
    /// <exception cref="ArgumentException">当起始向量为零向量时抛出（无法计算旋转角度）</exception>
    /// <remarks>
    /// 计算原理：
    /// 1. 利用向量点积计算 cosθ：cosθ = (start·target) / (|start| * |target|)
    /// 2. 利用向量叉积计算 sinθ：sinθ = (start×target) / (|start| * |target|)
    /// 3. 通过 atan2(sinθ, cosθ) 得到最终旋转角度（自动处理象限问题）
    /// 简化公式：θ = atan2(start.X*target.Y - start.Y*target.X, start.X*target.X + start.Y*target.Y)
    /// </remarks>
    public static float CalculateRotationRadian(this PEVector startVector, PEVector targetVector)
    {
        // 增加零向量校验，避免无意义的计算结果
        if (startVector.X == 0 && startVector.Y == 0)
        {
            throw new ArgumentException("起始向量不能为零向量，无法计算旋转角度", nameof(startVector));
        }

        // 计算叉积（sinθ 分子）和点积（cosθ 分子）
        double crossProduct = startVector.X * targetVector.Y - startVector.Y * targetVector.X;
        double dotProduct = startVector.X * targetVector.X + startVector.Y * targetVector.Y;

        return (float)Math.Atan2(crossProduct, dotProduct);
    }
}