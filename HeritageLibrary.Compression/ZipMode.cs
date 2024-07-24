using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Compression;

/// <summary>
/// <see cref="ZipMode"/> 列挙型は、ZIP ファイルにデータを書き込む規則を示す識別子を表します。
/// </summary>
[Flags]
public enum ZipMode : byte
{
	/// <summary>
	/// 新しくデータを作成することを指定します。データが既に存在する場合は <see cref="System.IO.IOException"/> がスローされます。
	/// </summary>
	Create = 0x01,
	/// <summary>
	/// データが既に存在する場合は新しくデータを作り直すことを指定します。データが存在しない場合は <see cref="System.IO.IOException"/> がスローされます。
	/// </summary>
	Update = 0x02,
	/// <summary>
	/// データが既に存在する場合はデータの末尾から追記することを指定します。データが存在しない場合は <see cref="System.IO.IOException"/> がスローされます。
	/// </summary>
	Append = 0x04,
	/// <summary>
	/// 新しくデータを作成することを指定します。または、データが既に存在する場合は新しくデータを作り直すことを指定します。
	/// </summary>
	CreateOrUpdate = Create | Update,
	/// <summary>
	/// 新しくデータを作成することを指定します。または、データが既に存在する場合はデータの末尾から追記することを指定します。
	/// </summary>
	CreateOrAppend = Create | Append,
}
