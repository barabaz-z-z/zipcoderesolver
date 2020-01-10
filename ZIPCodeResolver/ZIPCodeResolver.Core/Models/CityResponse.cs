using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIPCodeResolver.Core.Models
{
    public sealed class CityResponse : ICityResponse
    {
        public City City { get; set; }
    }
}
