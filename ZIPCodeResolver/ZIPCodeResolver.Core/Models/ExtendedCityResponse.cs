using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIPCodeResolver.Core.Models
{
    public sealed class ExtendedCityResponse : ICityResponse
    {
        public City City { get; set; }
        public SelectionInfo SelectionInfo { get; set; }
    }
}
