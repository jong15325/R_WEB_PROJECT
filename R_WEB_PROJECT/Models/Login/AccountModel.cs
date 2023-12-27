using System.ComponentModel.DataAnnotations;

namespace R_WEB_PROJECT.Models.Login
{
	public class AccountModel
	{

		[Key]
		public string aNo { get; set; }

		[Required(ErrorMessage = "이메일 항목이 비어있네요")] // 필수 입력 필드
		[EmailAddress(ErrorMessage = "이메일 형식이 올바르지 않아요")] // 이메일 주소 형식 검사
		public string aId { get; set; }

		[Required(ErrorMessage = "비밀번호를 입력하지 않았어요")] // 필수 입력 필드
		[DataType(DataType.Password)] // 비밀번호 타입 지정
		public string aPassword { get; set; }

		[Required(ErrorMessage = "이름이 알고싶어요")] // 필수 입력 필드
		public string aName { get; set; }
	}
}
