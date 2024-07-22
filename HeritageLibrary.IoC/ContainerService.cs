using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.IoC;

/// <summary>
/// <see cref="ContainerService"/> クラスは、シンプルな機能を提供する DI サービスに関するパラメーターです。
/// </summary>
public class ContainerService : IContainerService
{
	public Type Type { get; init; }
	public string Name { get; init; }
	public object Instance { get; init; }

	/// <summary>
	/// <see cref="ContainerService"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="type">インスタンスの型。</param>
	/// <param name="name">インスタンスの名前。</param>
	/// <param name="instance">コンテナに格納するインスタンス。</param>
	public ContainerService(Type type, string name, object instance)
	{
		Type = type;
		Name = name;
		Instance = instance;
	}
}
