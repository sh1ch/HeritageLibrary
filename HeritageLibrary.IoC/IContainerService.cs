using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageLibrary.IoC;

/// <summary>
/// <see cref="IContainerService"/> インターフェースは、DI サービスに関するパラメーターを定義します。
/// </summary>
public interface IContainerService
{
	/// <summary>
	/// データの型を取得します。
	/// </summary>
	public Type Type { get; }

	/// <summary>
	/// データを取得するためのインスタンス名を取得します。
	/// </summary>
	public string Name { get; }
	
	/// <summary>
	/// データのインスタンスを取得します。
	/// </summary>
	public object Instance { get; }
}
