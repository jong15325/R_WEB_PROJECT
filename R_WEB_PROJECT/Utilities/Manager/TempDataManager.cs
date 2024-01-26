using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace R_WEB_PROJECT.Utilities.Manager
{
    public class TempDataManager<T>
    {
        private readonly ITempDataDictionary _tempData;

        public TempDataManager(ITempDataDictionary tempData)
        {
            _tempData = tempData;
        }

        public T Model
        {
            get => _tempData.ContainsKey(typeof(T).Name) ? JsonSerializer.Deserialize<T>((string)_tempData[typeof(T).Name]) : default;
            set => _tempData[typeof(T).Name] = JsonSerializer.Serialize(value);
        }
    }
}