using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Core.Models;
using ZIPCodeResolver.Services.Handlers;

namespace ZIPCodeResolver.Services
{
    public sealed class CityService : ICityService
    {
        private readonly ICityHandler _cityHandler;
        private readonly IConfigurationService _configurationService;

        public CityService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            _cityHandler = new SetCityBaseDetailsHandler(_configurationService);
            var setCityLocationHandler = new SetCityLocationHandler(_configurationService);
            var setCityElevationHandler = new SetCityElevationHandler(_configurationService);
            var setCityTimeZoneHandler = new SetCityTimeZoneHandler(_configurationService);
            var setCityWeatherHandler = new SetCityWeatherHandler(_configurationService);

            _cityHandler.SetNext(setCityLocationHandler);
            setCityLocationHandler.SetNext(setCityElevationHandler);
            setCityElevationHandler.SetNext(setCityTimeZoneHandler);
            setCityTimeZoneHandler.SetNext(setCityWeatherHandler);
        }

        public async Task<City> GetCityAsync(string postalCode)
        {
            var city = await _cityHandler.Handle(new City { PostalCode = postalCode });

            return city;
        }
    }
}
