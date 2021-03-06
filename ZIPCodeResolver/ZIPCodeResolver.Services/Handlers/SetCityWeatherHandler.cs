﻿using System;
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
        private readonly IConfigurationService _configuration;
        private readonly HttpClient _httpClient;

        public SetCityWeatherHandler(IConfigurationService configurationService)
            : base()
        {
            _configuration = configurationService;
            _httpClient = new HttpClient();
        }

        public override async Task<ICityResponse> Handle(ICityResponse response)
        {
            try
            {
                var host = _configuration.GetValue("OpenWeatherMapAPIHost");
                var query = $"lat={response.City.Location.Latitude}&lon={response.City.Location.Longitude}";
                var key = _configuration.GetValue("OpenWeatherMapAPIKey");
                var result = await _httpClient.GetStringAsync($"{host}?{query}&appid={key}&units=metric");
                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                response.City.Temperature = rootObject.Main.Temp;
            }
            catch { }

            return await base.Handle(response);
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
