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
    public sealed class SetCityTimeZoneHandler : CityHandler
    {
        private HttpClient _httpClient = new HttpClient();

        public SetCityTimeZoneHandler(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public override async Task Handle(City city)
        {
            try
            {
                var timestamp = DateTime.Now.Millisecond;
                var query = $"location={city.Location.Latitude},{city.Location.Longitude}&timestamp={timestamp}";
                var result = await _httpClient.GetStringAsync(GoogleUriHelper.GetUri(GoogleServiceTypes.Timezone, query));

                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                if (rootObject.Status == GoogleAPIStatuses.OK)
                {
                    city.TimeZoneName = rootObject.TimeZoneName;
                }
            }
            finally
            {
                await base.Handle(city);
            }
        }

        private class RootObject
        {
            public GoogleAPIStatuses Status { get; set; }
            public string TimeZoneId { get; set; }
            public string TimeZoneName { get; set; }
        }
    }
}
