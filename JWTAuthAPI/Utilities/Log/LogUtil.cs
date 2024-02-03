using log4net;
using System.Text;

namespace JWTAuthAPI.Utilities.Log
{
	public static class LogUtil
	{
		private static readonly ILog Logger = LogManager.GetLogger(typeof(LogUtil));

		//로그
		//Add log4net as logging provider
		//logger.Info, Debug, Warn, Error, Fatal
		//{0} : 첫번째 매개변수,$ 포함한 {count} 변수 count


		public static void Info(string type, string message, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[").Append(type).Append("] ").Append(message);
			string resultMsg = sb.ToString();

			if (args != null)
				Logger.InfoFormat(resultMsg, args);
			else
				Logger.InfoFormat(resultMsg);
		}

		public static void Debug(string type, string message, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[").Append(type).Append("] ").Append(message);
			string resultMsg = sb.ToString();

			if (args != null)
				Logger.DebugFormat(resultMsg, args);
			else
				Logger.DebugFormat(resultMsg);
		}

		public static void Warn(string type, string message, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[").Append(type).Append("] ").Append(message);
			string resultMsg = sb.ToString();

			if (args != null)
				Logger.WarnFormat(resultMsg, args);
			else
				Logger.WarnFormat(resultMsg);
		}

		public static void Error(string type, string message, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[").Append(type).Append("] ").Append(message);
			string resultMsg = sb.ToString();

			if (args != null)
				Logger.ErrorFormat(resultMsg, args);
			else
				Logger.ErrorFormat(resultMsg);
		}

		public static void Fatal(string type, string message, params object[] args)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[").Append(type).Append("] ").Append(message);
			string resultMsg = sb.ToString();

			if (args != null)
				Logger.FatalFormat(resultMsg, args);
			else
				Logger.FatalFormat(resultMsg);
		}
	}
}
