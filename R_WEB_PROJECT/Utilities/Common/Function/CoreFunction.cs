using R_WEB_PROJECT.DTOs;
using R_WEB_PROJECT.Models.User;
using R_WEB_PROJECT.Utilities.Log;

namespace R_WEB_PROJECT.Utilities.Common.Function
{
    public class CoreFunction
    {
        public static bool IsUserLockCheck(AccountValidDTO account)
        {
            // UserLockAt 값이 null이거나 비어 있는지 확인
            if (account.AccountInfo.UserLockAt == null || account.AccountInfo.UserLockAt == DateTime.MinValue)
            {
                return false;
            }

            // 현재 날짜 및 시간 가져오기
            DateTime currentDate = DateTime.Now;

            // UserLockAt와 현재 날짜 및 시간 간의 차이 계산
            TimeSpan difference = currentDate.Subtract(account.AccountInfo.UserLockAt.Value);

            if (difference.TotalSeconds <= -1)
            {
                // 계정이 잠겨 있음
                return true;
            }

            // 그 외의 경우 계정이 잠겨 있지 않음
            return false;
        }
    }
}
