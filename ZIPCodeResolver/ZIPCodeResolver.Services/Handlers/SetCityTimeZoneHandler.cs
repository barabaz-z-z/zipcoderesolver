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
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;

        public SetCityTimeZoneHandler(IConfigurationService configurationService)
            : base()
        {
            _configurationService = configurationService;
            _httpClient = new HttpClient();
        }

        public override async Task<ICityResponse> Handle(ICityResponse response)
        {
            try
            {
                var timestamp = DateTime.Now.Millisecond;
                var query = $"location={response.City.Location.Latitude},{response.City.Location.Longitude}&timestamp={timestamp}";
                var uriHelper = new GoogleUriHelper(_configurationService);
                var result = await _httpClient.GetStringAsync(uriHelper.GetUri(GoogleServiceTypes.Timezone, query));

                var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                if (rootObject.Status == GoogleAPIStatuses.OK)
                {
                    response.City.TimeZoneName = rootObject.TimeZoneName;
                }
            }
            catch { }

            return await base.Handle(response);
        }

        private class RootObject
        {
            public GoogleAPIStatuses Status { get; set; }
            public string TimeZoneId { get; set; }
            public string TimeZoneName { get; set; }
        }
    }
}
