using VladimirTripAdvisor.ViewModels;

namespace VladimirTripAdvisor.Logic.Application
{
    public class ApplicationJSONSerialization
    {
        public string SerializeApplication(ApplicationAccountViewModel application)
        {
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            string result = JsonSerializer.Serialize(application, options);
            return result;
        }

        public ApplicationAccountViewModel DeserializeApplication(string applicationData)
        {
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            ApplicationAccountViewModel application = JsonSerializer.Deserialize<ApplicationAccountViewModel>(applicationData);
            return application;
        }
    }
}
