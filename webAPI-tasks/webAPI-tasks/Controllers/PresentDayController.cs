using BOL;
using BLL;
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
    public class PresentDayController : ApiController
    {
        [HttpGet]
        [Route("api/getAllPressentGroupByUser")]
        /// <summary>
        /// get all presentDays group by user
        /// </summary>
        /// <returns>list presentDays group by user</returns>
        
        public HttpResponseMessage GetAllPressentGroupByUser()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<PresentDay>>(LogicPresentDay.GetAllPresents(), new JsonMediaTypeFormatter())
            };
        }


        [HttpGet]
        [Route("api/getAllPressentGroupByUserAndProject")]
        /// <summary>
        /// get all presentDays group by user
        /// </summary>
        /// <returns>list presentDays group by user</returns>

        public HttpResponseMessage getAllPressentGroupByUserAndProject()
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<PresentDay>>(LogicPresentDay.GetAllPresents(), new JsonMediaTypeFormatter())
            };
        }


        [HttpGet]
        [Route("api/getPressentUserByProject/{idWorker}/{idProject}")]
        // GET: api/PresentDay/5/5
        /// <summary>
        /// get presentDay by idWorker and idProject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage getPressentUserByProject(int idWorker,int idProject)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<PresentDay>(LogicPresentDay.GetPresent(idWorker,idProject), new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        [Route("api/getAllPressentsUser/{idWorker}")]
        public HttpResponseMessage Get(int idWorker)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<PresentDay>(LogicPresentDay.GetPresentByWorkerId(idWorker), new JsonMediaTypeFormatter())
            };
        }
        // POST: api/PresentDay
        public HttpResponseMessage Post([FromBody]PresentDay value)
        {
            if (ModelState.IsValid)
            {
                return (LogicPresentDay.AddPresent(value)) ?
                   new HttpResponseMessage(HttpStatusCode.Created) :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                   {
                       Content = new ObjectContent<String>("Can not add to DB", new JsonMediaTypeFormatter())
                   };
            };

            List<string> ErrorList = new List<string>();

            //if the code reached this part - the user is not valid
            foreach (var item in ModelState.Values)
                foreach (var err in item.Errors)
                    ErrorList.Add(err.ErrorMessage);

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<List<string>>(ErrorList, new JsonMediaTypeFormatter())
            };

        }

        // PUT: api/PresentDay
        public HttpResponseMessage Put([FromBody]PresentDay value)
        {

            if (ModelState.IsValid)
            {
                return (LogicPresentDay.UpdatePresent(value)) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not update in DB", new JsonMediaTypeFormatter())
                    };
            };

            List<string> ErrorList = new List<string>();

            //if the code reached this part - the user is not valid
            foreach (var item in ModelState.Values)
                foreach (var err in item.Errors)
                    ErrorList.Add(err.ErrorMessage);

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<List<string>>(ErrorList, new JsonMediaTypeFormatter())
            };
        }

        // DELETE: api/PresentDay/4
        public HttpResponseMessage Delete(int id)
        {
            return (LogicPresentDay.RemovePresent(id)) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not remove from DB", new JsonMediaTypeFormatter())
                    };
        }
    }
}
