using Newtonsoft.Json.Linq;
using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Logic.Application
{
    public class ApplicationJSONSerialization
    {
        public string SerializeApplication<T>(T application)
        {
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            string result = JsonSerializer.Serialize(application, options);
            return result;
        }

        public T DeserializeApplication<T>(string applicationData, ApplicationType type)
        {
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            if(type == ApplicationType.UpgradeAccount)
            {
                T application = JsonSerializer.Deserialize<T>(applicationData);
               return (T)Convert.ChangeType(application, typeof(T));
            }
            else
            {
                T application = JsonSerializer.Deserialize<T>(applicationData);
                return (T)Convert.ChangeType(application, typeof(T));
            }

        }
    }
}
