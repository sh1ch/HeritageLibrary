using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.ExpressionParser;

/// <summary>
/// <see cref="TextToNode"/> クラスは、テキストで表現された四則演算を含んだ数式を <see cref="Node"/> データに変換するクラスです。。
/// </summary>
public class TextToNode
{
    /// <summary>
    /// <see cref="TextToNode"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public TextToNode()
    {
    }

    /// <summary>
    /// 指定した数式を表すテキストをノードに分解します。
    /// </summary>
    /// <param name="text">数式を表すテキスト。</param>
    /// <param name="parenthesis">括弧の代替文字。</param>
    /// <returns>ノード。分解に失敗したときは <c>null</c> を返却します。</returns>
    public Node Parse(string text, char parenthesis = '#')
    {
        var root = new Node(parenthesis);
        var target = root;

        // スペースを削除
        var c = text.Replace(" ", "");

        for (int i = 0; i < c.Length; i++)
        {
            switch (c[i])
            {
                case '(':
                    target.Formula += target.Parenthesis;

                    // 子ノードを追加
                    var newNode = new Node(parenthesis);
                    newNode.Parent = target;
                    target.Childs.Add(newNode);
                    target = newNode;
                    break;
                case ')':
                    // 現在のノードを親に戻す
                    target = target.Parent;
                    break;
                default:
                    target.Formula += c[i];
                    break;
            }
        }

        return root;
    }
}
