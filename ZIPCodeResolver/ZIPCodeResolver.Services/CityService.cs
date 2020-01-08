using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Core.Models;
using ZIPCodeResolver.Services.Handlers;

namespace ZIPCodeResolver.Services
{
    public sealed class CityService : ICityService
    {
        private readonly IConfigurationService _configurationService;

        public CityService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<City> GetCityAsync(string postalCode)
        {
            var city = new City { PostalCode = postalCode };

            city.Name += _configurationService.GetValue("sdfsdf");

            var setBaseCityDetailsHandler = new SetCityBaseDetailsHandler(_configurationService);
            var setCityLocationHandler = new SetCityLocationHandler(_configurationService);
            var setCityElevationHandler = new SetCityElevationHandler(_configurationService);
            var setCityTimeZoneHandler = new SetCityTimeZoneHandler(_configurationService);
            var setCityWeatherHandler = new SetCityWeatherHandler(_configurationService);

            setBaseCityDetailsHandler.SetNext(setCityLocationHandler);
            setCityLocationHandler.SetNext(setCityElevationHandler);
            setCityElevationHandler.SetNext(setCityTimeZoneHandler);
            setCityTimeZoneHandler.SetNext(setCityWeatherHandler);

            await setBaseCityDetailsHandler.Handle(city);


            return city;
        }
    }
}
