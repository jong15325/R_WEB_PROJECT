using R_WEB_PROJECT.Utilities.Generator;
using R_WEB_PROJECT.Utilities.Log;
using System.Text;

namespace R_WEB_PROJECT.Utilities.password
{
	public class PasswordHasher
	{
		public static string HashPassword(string password, string a_password_salt, bool isCreate)
		{
			Log.Log.Debug("SECUTIRY", $"password = {password} / a_password_salt = {a_password_salt} / isCreate = {isCreate}");
			string salt = a_password_salt;
			if (isCreate) salt = SaltGenerator.GenerateRandomSalt(32);
			Log.Log.Debug("SECUTIRY", $"Salt Result = {salt}");
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
				string hash = Convert.ToBase64String(hashedBytes);
				return hash;
			}
		}
	}
}
