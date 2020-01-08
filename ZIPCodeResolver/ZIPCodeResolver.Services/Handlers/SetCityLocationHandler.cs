using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Core.Models;
using ZIPCodeResolver.Services.GoogleCore;

namespace ZIPCodeResolver.Services.Handlers
{
    public sealed class SetCityLocationHandler : CityHandler
    {
        private HttpClient _httpClient = new HttpClient();

        public SetCityLocationHandler(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public override async Task Handle(City city)
        {
            try
            {
                var result = await _httpClient.GetStringAsync($"{GoogleContants.APIHost}/geocode/{GoogleContants.Format}?place_id={city.Id}&key={GoogleContants.APIKey}");

                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                if (rootObject.Status == GoogleAPIStatuses.OK)
                {
                    var location = rootObject.Results.First().Geometry.Location;
                    city.Location = new Core.Models.Location
                    {
                        Latitude = location.Lat,
                        Longitude = location.Lng
                    };
                }
            }
            finally
            {
                await base.Handle(city);
            }
        }

        private class Location
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        private class Geometry
        {
            public Location Location { get; set; }
        }

        private class Result
        {
            public Geometry Geometry { get; set; }
        }

        private class RootObject
        {
            public List<Result> Results { get; set; }
            public GoogleAPIStatuses Status { get; set; }
        }
    }
}
