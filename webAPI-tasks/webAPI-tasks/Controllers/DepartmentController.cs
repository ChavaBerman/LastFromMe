using BLL;
using BOL;
using BOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace webAPI_tasks.Controllers
{
    public class statusController : ApiController
    {
        [HttpGet]
        [Route("api/Status/GetAllstatuses")]
        /// <summary>
        /// get all statuss
        /// </summary>
        /// <returns>list statuss </returns>

        public HttpResponseMessage GetAllstatuses()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Status>>(Logicstatus.GetAllstatuses(), new JsonMediaTypeFormatter())
            };
        }

    }
}
