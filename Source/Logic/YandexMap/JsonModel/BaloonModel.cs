namespace VladimirTripAdvisor.Logic.YandexMap.JsonModel
{
    public class BaloonModel
    {
        public BaloonModel()
        {

        }
        public BaloonModel(Feature[] ballonCollections)
        {
            type = "FeatureCollection";
            features = ballonCollections;

        }
        public string type { get; set; }
        public IList<Feature> features { get; set; }
    }

    public class Feature
    {
        public Feature()
        {

        }
        public Feature(long objectId, string latitude, string longitude,
                string contentHeader, string contentBody, string contentFooter = null)
        {
            id = objectId;
            type = "Feature";
            geometry = new Geometry(latitude, longitude);
            properties = new Properties(contentHeader, contentBody, contentFooter);
        }
        public string type { get; set; }
        public long id { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public Geometry()
        {

        }
        public Geometry(string latitude, string longitude)
        {
            type = "Point";
            coordinates = new[] { double.Parse(latitude.Replace('.',',')),
                double.Parse(longitude.Replace('.',',')) };
        }
        public string type { get; set; }
        
        public double[] coordinates { get; set; }
    }

    public class Properties
    {
        public Properties()
        {

        }
        public Properties(string contentHeader, string contentBody, string contentFooter)
        {
            balloonContentHeader = contentHeader;
            balloonContentBody = contentBody;
            balloonContentFooter = contentFooter;
        }
        public string balloonContentHeader { get; set; }
        public string balloonContentBody { get; set; }
        public string balloonContentFooter { get; set; }
        public string? clustercaption { get; set; }
        public string? hintcontent { get; set; }
    }
}

