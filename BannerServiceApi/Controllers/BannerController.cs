using System;
using System.Net;
using System.Net.Http;
using BannerServiceApi.DataBaseInterfacer;
using BannerServiceApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BannerServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IDataBaseInterfacer<IBanner> _databaseInterfacer = new MongoDataBaseInterfacer();

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Banner banner)

        {
            try
            {
                if (_databaseInterfacer == null) return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                //TODO:Validation of banner's html if it is html
                _databaseInterfacer.AddAsync(banner)?.GetAwaiter().GetResult();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get(int bannerId)
        {
            if (_databaseInterfacer == null) return new ContentResult() { StatusCode = (int)HttpStatusCode.InternalServerError };
            var banner = _databaseInterfacer.ReadAsync(bannerId)?.GetAwaiter().GetResult();
            if (banner == null) return new ContentResult() { StatusCode = (int)HttpStatusCode.BadRequest };
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = banner.Html
            };
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int bannerId)
        {
            if (_databaseInterfacer == null) return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            _databaseInterfacer.DeleteAsync(bannerId)?.GetAwaiter().GetResult();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}