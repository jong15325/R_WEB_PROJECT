using R_WEB_PROJECT.Utilities.Generator;
using System.Text;

namespace R_WEB_PROJECT.Utilities.password
{
	public class PasswordManager
	{
		public static string HashPassword(string inputPassword, string UserStoreSalt, bool isCreate)
		{
			Log.Log.Debug("SECUTIRY", $"inputPassword = {inputPassword} / UserStoreSalt = {UserStoreSalt} / isCreate = {isCreate}");

			string salt = UserStoreSalt;

			if (isCreate) 
			{
				salt = SaltGenerator.GenerateRandomSalt(32);
				Log.Log.Debug("SECUTIRY", $"New Salt = {salt}");
			}

			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword + salt));
				string hash = Convert.ToBase64String(hashedBytes);
				return hash;
			}
		}

		public static bool VerifyPassword(string hashedPassword, string storedPassword)
		{
			return hashedPassword == storedPassword;
		}
	}
}
