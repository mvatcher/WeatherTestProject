namespace WeatherProject
{
    public class OpenWeatherResponse
    {
        public string Name { get; set; }

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }

        public Wind Wind { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }

    }

    public class Main
    {
        public string Temp { get; set; }
        //public int TemperatureF => 32 + (int)(Double.Parse(Temp) / 0.5556);
    }

    public class Wind
    {
        public string Speed { get; set; }
        public string Deg { get; set; }
    }
}
