using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageLibrary.IoC;

/// <summary>
/// <see cref="ContainerServiceCollection"/> クラスは、シンプルな機能を提供する DI コレクションです。
/// </summary>
public class ContainerServiceCollection : IContainerServiceCollection
{
	private readonly List<IContainerService> _ContainerServices = new List<IContainerService>();

	public IEnumerable<IContainerService> ContainerServices => _ContainerServices;

	public int Count
	{
		get { lock (_ContainerServices) { return _ContainerServices.Count; } }
	}

	/// <summary>
	/// <see cref="ContainerServiceCollection"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	public ContainerServiceCollection()
	{
	}

	public void AddSingleton<T>(T instance, string name)
	{
		if (instance == null)
		{
			throw new ArgumentNullException($"Failed to add a instance. (Type={typeof(T)}, Name={name}) is null.");
		}

		AddSingleton(typeof(T), instance, name);
	}

	public void AddSingleton(Type type, object instance,string name)
	{
		if (!IsSingle(type, name))
		{
			throw new ArgumentException($"Failed to add a instance. (Type={type}, Name={name}) is already added.");
		}

		var newService = new ContainerService(type, name, instance);

		lock (_ContainerServices)
		{
			_ContainerServices.Add(newService);
		}
	}

	public T Resolve<T>(string name)
	{
		var service = _ContainerServices.FirstOrDefault(p => p.Type == typeof(T) && p.Name == name);

		if (service == null)
		{
			throw new NullReferenceException($"Failed to resolve. (Type={typeof(T)}, Name={name}) is NOT added in collection.");
		}

		return (T)service.Instance;
	}

	private bool IsSingle(Type type, string name)
	{
		var sameServices = _ContainerServices.Select(p => p.Type == type && p.Name == name);

		return sameServices.Count() == 0;
	}
}
