using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Security;
/// <summary>
/// <see cref="Pbkdf2"/> クラスは、 Password-Based Key Drivation Function 2 (パスワードベース鍵導出関数 2)  ハッシュを生成します。
/// </summary>
public class Pbkdf2
{
    readonly int KEY_SIZE = 32;  // (JUNE 2016)
    readonly int INDICATION_ITERATIONS = 20000; // 目安になる反復処理回数 (JUNE 2016)

    /// <summary>
    /// <see cref="Pbkdf2"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    public Pbkdf2()
    {

    }

    /// <summary>
    /// <see cref="Pbkdf2"/> クラスの新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="size">キーの大きさ。</param>
    /// <param name="iterations">イテレーションの回数。</param>
    public Pbkdf2(int size, int iterations)
    {
        KEY_SIZE = size;
        INDICATION_ITERATIONS = iterations;
    }

    /// <summary>
    /// 疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <param name="iterations">演算の反復処理回数。</param>
    /// <param name="keySize">キーの大きさ。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public byte[] Generate(string password, string salt, int iterations, int keySize) =>
        new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), iterations).GetBytes(keySize);

    /// <summary>
    /// 疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <param name="iterations">演算の反復処理回数。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public byte[] Generate(string password, string salt, int iterations) =>
        Generate(password, salt, iterations, KEY_SIZE);

    /// <summary>
    /// 疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public byte[] Generate(string password, string salt) =>
        Generate(password, salt, INDICATION_ITERATIONS, KEY_SIZE);

    /// <summary>
    /// Base64 でエンコードされた疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <param name="iterations">演算の反復処理回数。</param>
    /// <param name="keySize">キーの大きさ。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public string GenerateBase64(string password, string salt, int iterations, int keySize) =>
        Convert.ToBase64String(Generate(password, salt, iterations, keySize));

    /// <summary>
    /// Base64 でエンコードされた疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <param name="iterations">演算の反復処理回数。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public string GenerateBase64(string password, string salt, int iterations) =>
        Convert.ToBase64String(Generate(password, salt, iterations, KEY_SIZE));

    /// <summary>
    /// Base64 でエンコードされた疑似ランダム キー（ハッシュ値）を生成します。
    /// </summary>
    /// <param name="password">パスワード。</param>
    /// <param name="salt">キーを派生させるための SALT 値。</param>
    /// <returns>疑似ランダム キー（ハッシュ値）。</returns>
    public string GenerateBase64(string password, string salt) =>
        Convert.ToBase64String(Generate(password, salt, INDICATION_ITERATIONS, KEY_SIZE));
}
