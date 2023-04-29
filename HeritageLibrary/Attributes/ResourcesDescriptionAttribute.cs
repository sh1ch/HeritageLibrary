using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Globalization;

namespace Heritage.Attributes;

/// <summary>
/// <see cref="ResourcesDescriptionAttribute"/> クラスは、リソースのテキストデータを付与する属性クラスです。
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
public class ResourcesDescriptionAttribute : DescriptionAttribute
{
    private readonly string _Description;
    private Type _ResourceType { get; set; }

    /// <summary>
    /// <see cref="Resources"/> クラスのテキストデータを取得します。
    /// </summary>
    public override string Description
    {
        get
        {
            var resourceManager = _ResourceType.InvokeMember(
                @"ResourceManager",
                BindingFlags.GetProperty | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                null,
                new object[] { }) as ResourceManager;

            var cultureInfo = _ResourceType.InvokeMember(
                @"Culture",
                BindingFlags.GetProperty | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                null,
                new object[] { }) as CultureInfo;

            DescriptionValue = resourceManager?.GetString(DescriptionValue, cultureInfo) ?? "";

            return DescriptionValue;
        }
    }

    /// <summary>
    /// <see cref="ResourcesDescriptionAttribute"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="description">リソースデータの名前。</param>
    /// <param name="resourcesType">リソースの型。</param>
    public ResourcesDescriptionAttribute(string description, Type resourcesType) : base(description)
    {
        _Description = description;
        _ResourceType = resourcesType;
    }
}

public static class ResourcesDescriptionExtension
{
    /// <summary>
    /// <see cref="ResourcesDescriptionAttribute"/> 属性を持つプロパティ値の <see cref="ResourcesDescriptionAttribute.Description"/> の値を取得します。
    /// </summary>
    /// <param name="value"><see cref="ResourcesDescriptionAttribute"/> 属性を持つプロパティ値。</param>
    /// <returns>属性の値。値が存在しないときは、"" を返却します。</returns>
    public static string GetAttributeValue(this object value)
    {
        var attributeValue = "";
        var fieldInfo = value.GetType().GetField(value?.ToString() ?? "") as FieldInfo;

        if (fieldInfo != null)
        {
            var attribute = fieldInfo.GetCustomAttribute(
                typeof(ResourcesDescriptionAttribute),
                false) as ResourcesDescriptionAttribute;

            attributeValue = attribute?.Description ?? "";
        }

        return attributeValue;
    }
}
