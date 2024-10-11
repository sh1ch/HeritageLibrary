using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Credentials.UI;

namespace Heritage.Security.Credentials;

/// <summary>
/// <see cref="WindowsHello"/> クラスは、WindowsHello の機能をサポートします。
/// </summary>
public class WindowsHello
{
	/// <summary>
	/// Microsoft Passport PIN、Windows Hello、指紋リーダーなどの認証デバイスが使用できるかどうかを確認します。
	/// </summary>
	/// <returns>可用性チェック操作の結果を表す <see cref="UserConsentVerifierAvailability"/> の値。</returns>
	public static async Task<UserConsentVerifierAvailability> CheckAvailabilityAsync() => await UserConsentVerifier.CheckAvailabilityAsync();

	public static async Task<bool> Request(string message)
	{
		var isVerified = false;
		var ucvAvailability = await UserConsentVerifier.CheckAvailabilityAsync();

		if (ucvAvailability == UserConsentVerifierAvailability.Available)
		{
			var consentResult = await UserConsentVerifier.RequestVerificationAsync(message);

			if (consentResult == UserConsentVerificationResult.Verified)
			{
				isVerified = true;
			}
		}

		return isVerified;
	}
}
