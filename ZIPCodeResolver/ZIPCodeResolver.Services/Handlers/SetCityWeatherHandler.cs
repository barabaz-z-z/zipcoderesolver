using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Core.Models;

namespace ZIPCodeResolver.Services.Handlers
{
    public sealed class SetCityWeatherHandler : CityHandler
    {
        private HttpClient _httpClient = new HttpClient();

        public SetCityWeatherHandler(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public override async Task Handle(City city)
        {
            try
            {
                var apikey = "7ad425a6ec3f2b4240cd76d954f60f9f";
                var result = await _httpClient.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?lat={city.Location.Latitude}&lon={city.Location.Longitude}&appid={apikey}&units=metric");
                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                city.Temperature = rootObject.Main.Temp;
            }
            finally
            {
                await base.Handle(city);
            }
        }

        public class Main
        {
            public double Temp { get; set; }
        }

        public class RootObject
        {
            public Main Main { get; set; }
        }
    }
}
