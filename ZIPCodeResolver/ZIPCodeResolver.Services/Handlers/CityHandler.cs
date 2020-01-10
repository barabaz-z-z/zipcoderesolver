using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIPCodeResolver.Core.Models;

namespace ZIPCodeResolver.Services.Handlers
{
    public abstract class CityHandler : ICityHandler
    {
        private ICityHandler _cityHandler;

        public virtual async Task<City> Handle(City city)
        {
            if (_cityHandler != null)
            {
                return await _cityHandler.Handle(city);
            }

            return city;
        }

        public ICityHandler SetNext(ICityHandler handler)
        {
            _cityHandler = handler;

            return _cityHandler;
        }
    }
}
