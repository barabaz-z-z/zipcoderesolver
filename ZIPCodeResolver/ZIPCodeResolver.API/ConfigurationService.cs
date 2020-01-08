using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using ZIPCodeResolver.Core;

namespace ZIPCodeResolver.API
{
    public sealed class ConfigurationService : IConfigurationService
    {
        public string GetValue(string key)
        {
            var settings = ConfigurationManager.AppSettings as NameValueCollection;

            return $"Get value and {key}";
        }
    }
}