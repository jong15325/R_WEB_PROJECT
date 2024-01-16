using Microsoft.Extensions.Localization;
using R_WEB_PROJECT.Resources;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class MessageManager
    {

        private readonly IStringLocalizer<SharedResource> _localizer;

        public MessageManager(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string GetMessage(string messageKey)
        {
            return _localizer[messageKey];
        }
    }
}
