using Microsoft.AspNetCore.Components.Web;
using NuGet.Protocol.Plugins;
using System.IO;
using System.Text;
using VladimirTripAdvisor.Logic.YandexMap.JsonModel;
using static System.Net.Mime.MediaTypeNames;

namespace VladimirTripAdvisor.Logic.YandexMap
{
    public class BaloonJson
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public BaloonJson(IWebHostEnvironment webHostEnvironment)
        {
            _appEnvironment = webHostEnvironment;
        }

        public void AddObject(ObjectOfVisitModel place, string imageBase64)
        {
            var objectBaloons = LoadJson();

            if (objectBaloons != null)
            {
                Feature baloon = new Feature(place.Id, place.Latitude, place.Longitude,
                    GenerateBaloonHeaderContent(place.Name), GenerateBaloonBodyContent(imageBase64, place.PlaceURL, place.PhoneNumber, place.Id));
                objectBaloons.features.Add(baloon);
                SaveJson(objectBaloons);
            }

        }

        private string GenerateBaloonHeaderContent(string placeName)
        {
            string content = $"<div class='d-flex justify-content-center'><p class='fw-bold text-primary' style='font-size: 26px;margin-bottom: 5px;'>{placeName}</p> </div>";
            return content;
        }

        private string GenerateBaloonBodyContent(string imageBase64, string placeURL, string phoneNumber, long placeId)
        {
            string content = $"<div class='d-flex justify-content-center'><img src='data:image/jpg;base64, {imageBase64}' style='object-fit: contain;height: 250px;' />" +
                $"</div><div class='d-flex justify-content-around' style='margin-top: 20px;'>" +
                $"<p class='fw-bold text-center d-flex flex-column justify-content-center' " +
                $"style='width: max-content;margin-left: 25px;margin-bottom: 0px;'>Веб-сайт:<a href='{placeURL}' " +
                $"style='padding-left: 0px;margin-left: 0px;'>{placeURL}</a></p><p class='fw-bold text-center' " +
                $"style='width: max-content;margin-left: 25px;margin-bottom: 15px;'>Номер телефона: <br />{phoneNumber}</p></div>" +
                $"<div class='d-flex justify-content-center'><a href='/Object/ViewObject/{placeId}' class='btn btn-primary'> Перейти </a></div>";
            return content;
        }


        private BaloonModel LoadJson()
        {
            string baloons;
            string pathJson = _appEnvironment.WebRootPath + "\\ymaps\\objects.json";
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            using (StreamReader streamReader = new StreamReader(pathJson, Encoding.UTF8))
            {
                baloons = streamReader.ReadToEnd();
            }

            BaloonModel baloonJson = JsonSerializer.Deserialize<BaloonModel>(baloons, options);

            return baloonJson;

        }

        private async Task SaveJson(BaloonModel baloons)
        {
            string pathJson = _appEnvironment.WebRootPath + "\\ymaps\\objects.json";
            var options = new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true};

            string baloonJsonString = JsonSerializer.Serialize(baloons,options);

            await File.WriteAllTextAsync(pathJson, baloonJsonString);


        }


    }
}
