using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZIPCodeResolver.Core.Models
{
    public sealed class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string TimeZoneName { get; set; }
        public double Elevation { get; set; }
        public double Temperature { get; set; }
        public Location Location { get; set; }
    }
}