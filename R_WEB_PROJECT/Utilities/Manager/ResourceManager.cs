using Microsoft.Extensions.Localization;
using R_WEB_PROJECT.Resources;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class ResourceManager
    {

        private readonly IStringLocalizer<SharedResource> _localizer;

        public ResourceManager(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        //Resource 메세지 가져온다
        public string GetMessage(string messageKey)
        {
            return _localizer[messageKey];
        }
    }
}
