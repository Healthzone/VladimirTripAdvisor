using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Logic.Application
{
    public class ApplicationJSONSerialization
    {
        public string SerializeApplication(ApplicationDataViewModel application)
        {
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            string result = JsonSerializer.Serialize(application, options);
            return result;
        }
    }
}
