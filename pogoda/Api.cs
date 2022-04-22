using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace pogoda
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Root
    {
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public int timezone { get; set; }
    }

    public class Api
    {
        public Root LoadData(string city, string apiKey)
        {
            string myJsonResponse = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + apiKey;
            HttpHelper helper = new HttpHelper();
            var data = helper.Json_Convert(myJsonResponse);
            var weather = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(data);

            return weather;
        }
    }
}