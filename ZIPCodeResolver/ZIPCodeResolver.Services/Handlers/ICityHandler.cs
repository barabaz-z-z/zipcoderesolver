using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIPCodeResolver.Core.Models;

namespace ZIPCodeResolver.Services.Handlers
{
    public interface ICityHandler
    {
        Task<City> Handle(City city);
        ICityHandler SetNext(ICityHandler handler);
    }
}
