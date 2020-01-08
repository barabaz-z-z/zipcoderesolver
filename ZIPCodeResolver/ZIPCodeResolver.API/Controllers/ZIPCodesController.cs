using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ZIPCodeResolver.Core;

namespace ZIPCodeResolver.API.Controllers
{
    public sealed class ZIPCodesController : ApiController
    {
        private const string ROUTE = RouteConstants.API + "/zipcodes";
        private const string SINGLE_ROUTE = ROUTE + "/{code}";

        private readonly ICityService _cityService;

        public ZIPCodesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route(SINGLE_ROUTE)]
        public async Task<IHttpActionResult> GetAsync([FromUri] string code)
        {
            var city = await _cityService.GetCityAsync(code);

            return Json(city);
        }
    }
}
