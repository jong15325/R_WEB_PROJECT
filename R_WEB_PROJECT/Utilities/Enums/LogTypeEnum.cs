using System.ComponentModel;

namespace R_WEB_PROJECT.Utilities.Enums
{
	public enum LogTypeEnum
	{
		[Description("System")]
		System,

		[Description("Sql")]
		Sql,

		[Description("Database")]
		Database,

		[Description("Security")]
		Security,

		[Description("Performance")]
		Performance,

		[Description("Network")]
		Network,
	}
}
