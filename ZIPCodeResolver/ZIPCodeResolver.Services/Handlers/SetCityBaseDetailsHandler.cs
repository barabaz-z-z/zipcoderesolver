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
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;

        public SetCityBaseDetailsHandler(IConfigurationService configurationService)
            : base()
        {
            _configurationService = configurationService;
            _httpClient = new HttpClient();
        }

        public override async Task<ICityResponse> Handle(ICityResponse response)
        {
            if (String.IsNullOrEmpty(response.City.PostalCode))
            {
                throw new Exception($"{nameof(City.PostalCode)} is not provided. Pass postal code as part of url path");
            }

            int predictionsNumber = 0;
            var query = $"input={response.City.PostalCode}&types=(regions)";
            var uriHelper = new GoogleUriHelper(_configurationService);
            var result = await _httpClient.GetStringAsync(uriHelper.GetUri(GoogleServiceTypes.Place, query));
            var rootObject = JsonConvert.DeserializeObject<RootObject>(result);

            if (rootObject.Status == GoogleAPIStatuses.OK)
            {
                predictionsNumber = rootObject.Predictions.Count();
                var predition = rootObject.Predictions.First();
                response.City.Id = predition.Reference;
                response.City.Name = predition.Description;
            }

            var handledResponse = await base.Handle(response);
            return new ExtendedCityResponse
            {
                City = handledResponse.City,
                SelectionInfo = new SelectionInfo
                {
                    Count = predictionsNumber
                }
            };
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
