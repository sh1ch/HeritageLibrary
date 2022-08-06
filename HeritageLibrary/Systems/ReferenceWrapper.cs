using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Systems;


/// <summary>
/// <see cref="ReferenceWrapper"/> クラスは、値型を参照型にするラッパー機能を提供します。
/// </summary>
public class ReferenceWrapper
{
    /// <summary>
    /// <see cref="ReferenceWrapper"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <typeparam name="T1">値型。</typeparam>
    /// <param name="value">値型の値。</param>
    /// <returns>生成したインスタンス。</returns>
    public static ReferenceWrapper<T1> Create<T1>(T1 value) where T1 : struct => new ReferenceWrapper<T1>(value);

    /// <summary>
    /// <see cref="ReferenceWrapper"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    private ReferenceWrapper()
    {

    }
}

/// <summary>
/// <see cref="ReferenceWrapper"/> クラスは、値型を参照型にするラッパー機能を提供します。
/// </summary>
public class ReferenceWrapper<T> where T : struct
{
    private T _Value;

    /// <summary>
    /// 値型の値を取得または設定します。
    /// </summary>
    public T Value
    {
        get => _Value;
        set => _Value = value;
    }

    /// <summary>
    /// <see cref="ReferenceWrapper"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public ReferenceWrapper() : this(default)
    {

    }

    /// <summary>
    /// <see cref="ReferenceWrapper"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="value">値型の値。</param>
    public ReferenceWrapper(T value)
    {
        Value = value;
    }
}
