using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageLibrary.IoC;

/// <summary>
/// <see cref="IContainerService"/> インターフェースは、DI コレクションに関するパラメーターを定義します。
/// </summary>
public interface IContainerServiceCollection
{
	IEnumerable<IContainerService> ContainerServices { get; }

	void AddSingleton<T>(T instance, string name);
	void AddSingleton(Type type, object instance, string name);

	T Resolve<T>(string name);
}
