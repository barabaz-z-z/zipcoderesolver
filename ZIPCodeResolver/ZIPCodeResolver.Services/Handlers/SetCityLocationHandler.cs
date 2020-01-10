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
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;

        public SetCityLocationHandler(IConfigurationService configurationService)
            : base()
        {
            _configurationService = configurationService;
            _httpClient = new HttpClient();
        }

        public override async Task<ICityResponse> Handle(ICityResponse response)
        {
            try
            {
                var query = $"place_id={response.City.Id}";
                var uriHelper = new GoogleUriHelper(_configurationService);
                var result = await _httpClient.GetStringAsync(uriHelper.GetUri(GoogleServiceTypes.Geocode, query));

                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                if (rootObject.Status == GoogleAPIStatuses.OK)
                {
                    var location = rootObject.Results.First().Geometry.Location;
                    response.City.Location = new Core.Models.Location
                    {
                        Latitude = location.Lat,
                        Longitude = location.Lng
                    };
                }
            }
            catch { }

            return await base.Handle(response);
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
