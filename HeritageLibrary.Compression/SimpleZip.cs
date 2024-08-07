﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Compression;

/// <summary>
/// <see cref="SimpleZip"/> クラスは、シンプルな ZIP ファイルの作成・操作する機能を提供するクラスです。
/// </summary>
public class SimpleZip
{
	/// <summary>
	/// ZIP ファイルのパスを取得します。
	/// </summary>
	public string FilePath { get; } = "";

	/// <summary>
	/// デフォルトの圧縮レベルを示す値を取得または設定します。
	/// </summary>
	public CompressionLevel Level { get; } = CompressionLevel.Fastest;

	/// <summary>
	/// ZIP ファイル内のエントリーの相対パスを表すテキストのコレクションを取得します。
	/// </summary>
	public IEnumerable<string> EntoriesCache { get; private set; } = new List<string>();

	/// <summary>
	/// エントリーのコレクション <see cref="Cache"/> を更新したときに発生するイベントです。
	/// </summary>
	public event EventHandler? EntoriesCacheUpdated;

	/// <summary>
	/// <see cref="SimpleZip"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="zipPath">ZIP ファイルのパス。</param>
	public SimpleZip(string zipPath) : this(zipPath, CompressionLevel.Fastest)
	{
	}

	/// <summary>
	/// <see cref="SimpleZip"/> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="zipPath">ZIP ファイルのパス。</param>
	/// <param name="level">圧縮レベル。</param>
	public SimpleZip(string zipPath, CompressionLevel level)
	{
		FilePath = zipPath;
		Level = level;

		if (!File.Exists(FilePath))
		{
			// 指定したファイルが存在しないときは、ZIP ファイルを作成する
			Create(FilePath);
		}
		else
		{
			// エントリーを更新する
			UpdateCache();
		}
	}

	/// <summary>
	/// 指定したエントリーのテキストデータのコレクションを取得します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <returns>エントリーのテキストデータのコレクション。</returns>
	public IEnumerable<string> ReadLine(string entryName)
	{
		var lines = new List<string>();

		using (var zip = ZipFile.OpenRead(FilePath))
		{
			var selectedFile = zip.Entries.FirstOrDefault(p => p.FullName == entryName);

			if (selectedFile == null)
			{
				throw new System.IO.FileNotFoundException(); 
			}

			using (var reader = new StreamReader(selectedFile.Open()))
			{
				string? line;

				while ((line = reader.ReadLine()) != null)
				{
					lines.Add(line);
				}
			}
		}

		return lines;
	}


	/// <summary>
	/// 指定したエントリーのテキストデータを取得します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <returns>エントリーのテキストデータ。</returns>
	public string ReadToEnd(string entryName)
	{
		using (var zip = ZipFile.OpenRead(FilePath))
		{
			var selectedFile = zip.Entries.FirstOrDefault(p => p.FullName == entryName);

			if (selectedFile == null)
			{
				throw new System.IO.FileNotFoundException(); 
			}

			using (var reader = new StreamReader(selectedFile.Open()))
			{
				return reader.ReadToEnd();
			}
		}
	}

	/// <summary>
	/// 指定した名前のディレクトリ―を、ZIP ファイル内に追加します。
	/// </summary>
	/// <param name="directoryName">追加するディレクトリーの名前。</param>
	public void MakeDirectory(string directoryName)
	{
		if (Exists(directoryName, true))
		{
			throw new System.IO.IOException("すでに同じ名前のファイル、または、フォルダーが存在しています。");
		}

		using (var zip = ZipFile.Open(FilePath, ZipArchiveMode.Update))
		{
			if (!directoryName.EndsWith(@"/") && !directoryName.EndsWith(@"\"))
			{
				directoryName += "/";
			}

			zip.CreateEntry(directoryName);
		}

		UpdateCache();
	}

	/// <summary>
	/// 指定したエントリーのデータを追加します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <param name="text">エントリーのデータ。</param>
	/// <param name="mode">エントリーの書き込み規則。</param>
	public void Write(string entryName, string text, ZipMode mode = ZipMode.Create)
	{
		Write(entryName, text, Level, mode);
	}

	/// <summary>
	/// 指定したエントリーのデータを追加します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <param name="text">エントリーのデータ。</param>
	/// <param name="level">圧縮方法。</param>
	/// <param name="mode">エントリーの書き込み規則。</param>
	public void Write(string entryName, string text, CompressionLevel level, ZipMode mode = ZipMode.Create)
	{
		var beforeText = "";

		if (Exists(entryName, true))
		{
			if (!mode.HasFlag(ZipMode.Update) && !mode.HasFlag(ZipMode.Append))
			{
				throw new System.IO.IOException("すでに同じ名前のファイル、または、フォルダーが存在しています。");
			}

			beforeText = ReadToEnd(entryName);

			Delete(entryName);
		}

		using (var zip = ZipFile.Open(FilePath, ZipArchiveMode.Update))
		{
			var newFile = zip.CreateEntry(entryName, level);
			
			using (var writer = new StreamWriter(newFile.Open(), System.Text.Encoding.UTF8))
			{
				if (mode.HasFlag(ZipMode.Append))
				{
					if (!string.IsNullOrEmpty(beforeText))
					{
						writer.Write(beforeText);
					}
				}

				writer.Write(text);
			}
		}

		UpdateCache();
	}

	/// <summary>
	/// 指定したエントリーのデータを追加し、続けて終端文字を追加します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <param name="text">エントリーのデータ。</param>
	/// <param name="mode">エントリーの書き込み規則。</param>
	public void WriteLine(string entryName, string text, ZipMode mode)
	{
		WriteLine(entryName, text, Level, mode);
	}

	/// <summary>
	/// 指定したエントリーのデータを追加し、続けて終端文字を追加します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <param name="text">エントリーのデータ。</param>
	/// <param name="level">圧縮方法。</param>
	/// <param name="mode">エントリーの書き込み規則。</param>
	public void WriteLine(string entryName, string text, CompressionLevel level, ZipMode mode)
	{
		var beforeText = "";

		if (Exists(entryName, true))
		{
			if (!mode.HasFlag(ZipMode.Update) && !mode.HasFlag(ZipMode.Append))
			{
				throw new System.IO.IOException("すでに同じ名前のファイル、または、フォルダーが存在しています。");
			}

			beforeText = ReadToEnd(entryName);

			Delete(entryName);
		}

		using (var zip = ZipFile.Open(FilePath, ZipArchiveMode.Update))
		{
			var newFile = zip.CreateEntry(entryName, level);

			using (var writer = new StreamWriter(newFile.Open(), System.Text.Encoding.UTF8))
			{
				if (mode.HasFlag(ZipMode.Append))
				{
					if (!string.IsNullOrEmpty(beforeText))
					{
						writer.Write(beforeText);
					}
				}

				writer.WriteLine(text);
			}
		}

		UpdateCache();
	}

	/// <summary>
	/// 指定したエントリーをZIP ファイルから削除します。
	/// </summary>
	/// <param name="entryName">削除するエントリーの相対パスを表すテキスト。</param>
	public void Delete(string entryName)
	{
		using (var zip = ZipFile.Open(FilePath, ZipArchiveMode.Update))
		{
			var selectedFile = zip.Entries.FirstOrDefault(p => p.FullName == entryName);

			if (selectedFile == null)
			{
				throw new System.IO.FileNotFoundException(); 
			}

			selectedFile.Delete();
		}

		UpdateCache();
	}

	/// <summary>
	/// 指定したエントリーの相対パスを表すテキストが、ZIP ファイル内に存在するかどうかを確認します。
	/// </summary>
	/// <param name="entryName">エントリーの相対パスを表すテキスト。</param>
	/// <param name="useCache">キャッシュを利用するかどうかを示す値。</param>
	/// <returns>エントリーの相対パスを表すテキストが存在するときは true、それ以外のとき false。</returns>
	public bool Exists(string entryName, bool useCache)
	{
		bool exists = false;

		if (useCache)
		{
			var selectedCache = EntoriesCache?.FirstOrDefault(p => p == entryName);

			exists = selectedCache != null;
		}
		else
		{
			using (var zip = ZipFile.OpenRead(FilePath))
			{
				var selectedFile = zip.Entries.FirstOrDefault(p => p.FullName == entryName);

				exists = selectedFile != null;
			}
		}

		return exists;
	}

	private IEnumerable<string> UpdateCache()
	{
		var files = new List<string>();

		using (var zip = ZipFile.OpenRead(FilePath))
		{
			foreach (var entry in zip.Entries)
			{
				files.Add(entry.FullName);
			}
		}

		EntoriesCache = files;
		EntoriesCacheUpdated?.Invoke(this, EventArgs.Empty);

		return files;
	}

	private void Create(string zipPath)
	{
		if (File.Exists(zipPath))
		{
			throw new System.IO.IOException("すでに同じ名前のファイル、または、フォルダーが存在しています。");
		}

		var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create);
		
		zip.Dispose();
	}
}
