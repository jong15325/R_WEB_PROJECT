using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
		}
	}
}
