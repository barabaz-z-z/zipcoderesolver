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
    public sealed class SetCityElevationHandler : CityHandler
    {
        private HttpClient _httpClient = new HttpClient();

        public SetCityElevationHandler(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public override async Task Handle(City city)
        {
            try
            {
                var query = $"locations={city.Location.Latitude},{city.Location.Longitude}";
                var result = await _httpClient.GetStringAsync(GoogleUriHelper.GetUri(GoogleServiceTypes.Elevation, query));
                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                if (rootObject.Status == GoogleAPIStatuses.OK)
                {
                    city.Elevation = rootObject.Results.First().Elevation;
                }
            }
            finally
            {
                await base.Handle(city);
            }
        }

        private class Result
        {
            public double Elevation { get; set; }
        }

        private class RootObject
        {
            public List<Result> Results { get; set; }
            public GoogleAPIStatuses Status { get; set; }
        }
    }
}
