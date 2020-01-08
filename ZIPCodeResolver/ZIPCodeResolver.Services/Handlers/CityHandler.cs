using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Core.Models;

namespace ZIPCodeResolver.Services.Handlers
{
    public abstract class CityHandler : ICityHandler
    {
        private ICityHandler _cityHandler;
        protected readonly IConfigurationService _configurationService;

        public CityHandler(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public virtual async Task Handle(City city)
        {
            if (_cityHandler != null)
            {
                await _cityHandler.Handle(city);
            }
        }

        public ICityHandler SetNext(ICityHandler handler)
        {
            _cityHandler = handler;

            return _cityHandler;
        }
    }
}
