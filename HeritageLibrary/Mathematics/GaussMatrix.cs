using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Mathematics;

/// <summary>
/// <see cref="GaussMatrix"/> クラスは、 Gauss-Jordan の消去法に適した行列を表現するクラスです。
/// </summary>
public class GaussMatrix
{
    private double[,] _A;

    /// <summary>
    /// 左辺の行列を取得または設定します。
    /// </summary>
    public double[,] A
    {
        get => _A;
        set => _A = value;
    }

    private double[] _B;

    /// <summary>
    /// 右辺の行列を取得または設定します。
    /// </summary>
    public double[] B
    {
        get => _B;
        set => _B = value;
    }

    /// <summary>
    /// 行列の次元数を取得します。
    /// </summary>
    public int Dimension { get; }

    /// <summary>
    /// <see cref="GaussMatrix"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="dimension">行列の次元数。</param>
    public GaussMatrix(int dimension)
    {
        _A = new double[dimension, dimension];
        _B = new double[dimension];

        Dimension = dimension;
    }

    /// <summary>
    /// 行列を Gauss-Jordan の消去法による対角化の計算をします。
    /// </summary>
    public void Calculate()
    {
        var a = A;
        var b = B;

        // 前方消去
        for (int i = 0; i < Dimension - 1; i++)
        {
            for (int j = i + 1; j < Dimension; j++)
            {
                var temp = a[j, i] / a[i, i];

                for (int k = i; k < Dimension; k++)
                {
                    a[j, k] -= a[i, k] * temp;
                }

                b[j] -= b[i] * temp;
            }
        }

        // 後方消去
        for (int i = Dimension - 1; i >= 0; i--)
        {
            b[i] /= a[i, i];
            a[i, i] = 1;

            for (int j = i - 1; j >= 0; j--)
            {
                var temp = a[j, i];

                a[j, i] = 0;
                b[j] -= b[i] * temp;

            }
        }
    }

    /// <summary>
    /// <see cref="A"/> 行列の 第 1 列の最大要素を第 1 行に交換します。
    /// </summary>
    /// <returns>ピボットの設定に成功したとき <c>true</c>。それ以外のとき <c>false</c>。</returns>
    public bool SetPivot()
    {
        int maxRow = 0;
        double maxValue = 0.0D;

        if (Dimension <= 0)
        {
            return false;
        }

        var a = A;
        var b = B;

        // 第一列目の最大の値を持つ行番号を取得する
        for (var i = 0; i < Dimension; i++)
        {
            if (a[i, 0] > maxValue)
            {
                maxValue = a[i, 0];
                maxRow = i;
            }
        }

        // 行の入れ替え
        if (maxRow > 0)
        {
            var swapA = new double[Dimension];

            // 行列 A の交換
            for (var i = 0; i < Dimension; i++)
            {
                swapA[i] = a[maxRow, i];
                a[maxRow, i] = a[0, i];
            }

            for (var i = 0; i < Dimension; i++)
            {
                a[0, i] = swapA[i];
            }

            // 行列 B の交換
            double swapB = b[maxRow];

            b[maxRow] = b[0];
            b[0] = swapB;
        }

        // 第一行の第一列目の要素が 0 のとき、例外
        if (a[0, 0] == 0) return false;

        return true;
    }
}
