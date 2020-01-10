using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIPCodeResolver.Core.Models;

namespace ZIPCodeResolver.Core
{
    public interface ICityService
    {
        Task<ICityResponse> GetCityResponseAsync(string postalCode);
    }
}
