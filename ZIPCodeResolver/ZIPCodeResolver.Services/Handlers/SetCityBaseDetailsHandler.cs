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
    public sealed class SetCityBaseDetailsHandler : CityHandler
    {
        private HttpClient _httpClient = new HttpClient();

        public SetCityBaseDetailsHandler(IConfigurationService configurationService) : base(configurationService)
        {
        }

        public override async Task Handle(City city)
        {
            if (String.IsNullOrEmpty(city.PostalCode))
            {
                throw new ArgumentException($"Argument {nameof(city)} has to contain certain {nameof(City.PostalCode)} value");
            }

            var query = $"input={city.PostalCode}&types=(regions)";
            var result = await _httpClient.GetStringAsync(GoogleUriHelper.GetUri(GoogleServiceTypes.Place, query));

            var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

            if (rootObject.Status == GoogleAPIStatuses.OK)
            {
                var predition = rootObject.Predictions.First();
                city.Id = predition.Reference;
                city.Name = predition.Description;

                await base.Handle(city);
            }
        }

        private class Prediction
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public string Reference { get; set; }
        }

        private class RootObject
        {
            public List<Prediction> Predictions { get; set; }
            public GoogleAPIStatuses Status { get; set; }
        }
    }
}
