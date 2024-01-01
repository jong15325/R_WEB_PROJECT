using System.Security.Cryptography;

namespace R_WEB_PROJECT.Utilities.Generator
{
	public class SaltGenerator
	{
		public static string GenerateRandomSalt(int size)
		{
			var rngCrypto = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rngCrypto.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}

	}
}
