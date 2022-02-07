using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Heritage.Security;
/// <summary>
/// <see cref="Sha512"/> クラスは、 Secure Hash Algorithm 512-bit ハッシュを生成します。
/// </summary>
public static class Sha512
{
    /// <summary>
    /// ランダムなバイト値 <see cref="byte[]"/> を取得します。
    /// </summary>
    /// <param name="byteSize">生成するバイト配列の大きさ。</param>
    /// <returns>暗号化に使用できる厳密な乱数。</returns>
    public static byte[] GetRandom(int byteSize) => RandomNumberGenerator.GetBytes(byteSize);

    /// <summary>
    /// Base64 でエンコードされたランダムな値のテキストを取得します。
    /// </summary>
    /// <param name="byteSize">生成するバイト配列の大きさ。</param>
    /// <returns>暗号化に使用できる厳密な乱数。</returns>
    public static string GetRandomBase64(int byteSize) => Convert.ToBase64String(GetRandom(byteSize));

    /// <summary>
    /// 指定したテキストからハッシュ値 <see cref="byte[]"/> を生成します。
    /// </summary>
    /// <param name="raw">ハッシュ値の基になる値。</param>
    /// <returns>生成したハッシュ値。</returns>
    public static byte[] Generate(string raw)
    {
        byte[] hash;
        var data = Encoding.UTF8.GetBytes(raw);

        using (var hashAlgorithm = SHA512.Create())
        {
            hash = hashAlgorithm.ComputeHash(data);
        }

        return hash;
    }

    /// <summary>
    /// 指定したテキストからハッシュ値を表すテキストを生成します。
    /// </summary>
    /// <param name="raw">ハッシュ値の基になる値。</param>
    /// <returns>生成したハッシュ値を表すテキスト。</returns>
    public static string GenerateBase64(string raw) => Convert.ToBase64String(Generate(raw));

}
