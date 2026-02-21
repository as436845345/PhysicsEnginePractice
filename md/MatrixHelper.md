# MatrixHelper.cs 源码说明

**文件路径**: [src/Helpers/MatrixHelper.cs](src/Helpers/MatrixHelper.cs#L1-L200)

## 概述
`MatrixHelper` 是一个静态辅助类，提供与二维向量旋转相关的核心方法：`RotateVector` 与 `CalculateRotationRadian`。前者使用二维旋转矩阵将向量绕原点按给定弧度旋转；后者返回从起始向量旋转到目标向量所需的带符号弧度（范围 $[-\pi,\pi]$）。

## 函数说明

- `RotateVector(this PEVector vector, float rotationRadian)`
  - 输入：`vector`（二维向量），`rotationRadian`（弧度，正值表示逆时针）
  - 输出：返回旋转后的新 `PEVector`（不修改原向量）
  - 实现要点：使用二维旋转矩阵对坐标进行变换。

- `CalculateRotationRadian(this PEVector startVector, PEVector targetVector)`
  - 输入：`startVector`、`targetVector`
  - 输出：返回旋转弧度 $\theta$，范围为 $[-\pi,\pi]$。
  - 异常：若 `startVector` 为零向量则抛出 `ArgumentException`。
  - 实现要点：通过向量的点积与 2D“叉积”计算 cosθ 与 sinθ，再用 `atan2` 得到带符号角度。

## 代码要点

- `RotateVector` 的计算：

  - X 分量：`(float)(vector.X * Math.Cos(rotationRadian) - vector.Y * Math.Sin(rotationRadian))`
  - Y 分量：`(float)(vector.X * Math.Sin(rotationRadian) + vector.Y * Math.Cos(rotationRadian))`

- `CalculateRotationRadian` 的计算：

  - 叉积（2D 标量）：`start.X * target.Y - start.Y * target.X`
  - 点积：`start.X * target.X + start.Y * target.Y`
  - 返回：`Math.Atan2(cross, dot)`

## 涉及的数学公式

以下公式以 KaTeX 形式列出，完全涵盖 `MatrixHelper.cs` 中使用或参照的数学表达。

- 二维旋转矩阵（将向量逆时针旋转角度 $\theta$）：
$$
\begin{bmatrix}
x'\\\\[4pt]
y'
\end{bmatrix}
=
\begin{bmatrix}
\cos\theta & -\sin\theta\\\\[4pt]
\sin\theta & \cos\theta
\end{bmatrix}
\begin{bmatrix}
x\\\\[4pt]
y
\end{bmatrix}
$$

- 展开后的坐标变换（用于 `RotateVector`）：
$$
x' = x\cos\theta - y\sin\theta
$$
$$
y' = x\sin\theta + y\cos\theta
$$

-- 向量点积与余弦关系（用于推导 $\cos\theta$）：
$$
\vec{a}\cdot\vec{b} = |\vec{a}|,|\vec{b}|\cos\theta
$$
因此
$$
\cos\theta = \dfrac{\vec{a}\cdot\vec{b}}{|\vec{a}|,|\vec{b}|}
$$

另外，点积也可以写成分量形式（二维情形）：
$$
\vec{a}\cdot\vec{b} = a_x b_x + a_y b_y
$$

2D 向量“叉积”标量形式（用于推导 $\sin\theta$）：
$$
\vec{a}\times\vec{b} = a_x b_y - a_y b_x
$$
因此
$$
\sin\theta = \dfrac{\vec{a}\times\vec{b}}{|\vec{a}|,|\vec{b}|}
$$

- 使用 `atan2` 合并获得带符号角度（`CalculateRotationRadian` 的核心公式）：
$$
\theta = \operatorname{atan2}\big(\vec{a}\times\vec{b},\vec{a}\cdot\vec{b}\big)
$$
等价于代码中的简化形式：
$$
\theta = \operatorname{atan2}(a_x b_y - a_y b_x,a_x b_x + a_y b_y)
$$

- 向量长度（在归一化或检验零向量时常用）：
$$
|\mathbf{a}| = \sqrt{a_x^2 + a_y^2}
$$