using System.ComponentModel.DataAnnotations;

namespace R_WEB_PROJECT.Models.Log
{
	public class LogLoginModel
	{
		public LogLoginModel()
		{
			Idx = 0;
			LoginUserId = string.Empty;
			LoginAt = DateTime.Now;
			LoginIp = string.Empty;
			LoginAgent = string.Empty;
			LoginStatusCode = 0;
			LoginMessage = string.Empty;
		}

		[Key]
		[Required]
		public int Idx { get; set; }

		[Required]
		[StringLength(50)]
		public string LoginUserId { get; set; }

		[Required]
		public DateTime LoginAt { get; set; }

		[Required]
		[StringLength(45)]
		public string LoginIp { get; set; }

		[Required]
		[MaxLength]
		public string LoginAgent { get; set; }

		[Required]
		public int LoginStatusCode { get; set; }

        [StringLength(200)]
        public string LoginMessage { get; set; }

        public override string ToString()
		{
			return $"Idx[{Idx}], LoginUserId[{LoginUserId}], LoginAt[{LoginAt}], LoginIp[{LoginIp}], LoginAgent[{LoginAgent}]," +
				$"LoginStatusCode[{LoginStatusCode}]";
		}
	}
}
