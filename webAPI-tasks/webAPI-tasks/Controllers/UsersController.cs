
using BOL;
using BLL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BOL.HelpModel;

namespace webAPI_tasks.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        [HttpGet]
        [Route("api/Users/getAllUsers")]   
        public HttpResponseMessage Get()
        {

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                
                Content = new ObjectContent<List<User>>(LogicManager.GetAllUsers(), new JsonMediaTypeFormatter())
            };
        }
        [HttpGet]
        [Route("api/Users/GetAllTeamHeads")]
        public HttpResponseMessage GetAllTeamHeads()
        {

            return new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new ObjectContent<List<User>>(LogicManager.GetAllTeamHeads(), new JsonMediaTypeFormatter())
            };
        }
        [HttpGet]
        [Route("api/Users/GetAllowedWorkers/{id}")]
        public HttpResponseMessage GetAllowedWorkers(int id)
        {

            return new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new ObjectContent<List<User>>(LogicManager.GetAllowedWorkers(id), new JsonMediaTypeFormatter())
            };
        }
        [HttpGet]
        [Route("api/Users/getUserDetails/{id}")]
        // GET: api/Users/5
        /// <summary>
        /// get details user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<User>(LogicManager.GetUserDetails(id), new JsonMediaTypeFormatter())
            };
        }

        [HttpGet]
        [Route("api/getProjectsById")]
        public HttpResponseMessage getProjectsById(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<User>(LogicManager.GetUserDetails(id), new JsonMediaTypeFormatter())
            };
        }




        [Route("api/addUser")]
        // POST: api/Users
        public HttpResponseMessage Post([FromBody]User value)
        {
            if (ModelState.IsValid)
            {
                return (LogicManager.AddUser(value)) ?
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

        [HttpPost]
        [Route("api/loginByPassword")]
        public HttpResponseMessage LoginByPassword([FromBody]LoginUser value)
        {
            if (ModelState.IsValid)
            {
                User user = LogicManager.GetUserDetailsByPassword(value.Password,value.UserName);
                //TODO:TOKEN
                return (user != null ?
                   new HttpResponseMessage(HttpStatusCode.Created)
                   {
                       Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter())
                   } :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                   {
                       Content = new ObjectContent<String>("This user does not exist", new JsonMediaTypeFormatter())
                   });
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
        [HttpPost]
        [Route("api/LoginByComputerUser")]
        public HttpResponseMessage LoginByComputerUser([FromBody]string ComputerUser)
        {

            User user = LogicManager.GetUserDetailsComputerUser(ComputerUser);
                //TODO:TOKEN
                return (user != null ?
                   new HttpResponseMessage(HttpStatusCode.Created)
                   {
                       Content = new ObjectContent<User>(user, new JsonMediaTypeFormatter())
                   } :
                   new HttpResponseMessage(HttpStatusCode.BadRequest)
                   {
                       Content = new ObjectContent<String>("Can not add to DB", new JsonMediaTypeFormatter())
                   });
        }

        // PUT: api/Users/5

        public HttpResponseMessage Put([FromBody]User value)
        {

            if (ModelState.IsValid)
            {
                return (LogicManager.UpdateUser(value)) ?
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


        // DELETE: api/Users/5
        public HttpResponseMessage Delete(int id)
        {
            return (LogicManager.RemoveUser(id)) ?
                    new HttpResponseMessage(HttpStatusCode.OK) :
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new ObjectContent<String>("Can not remove from DB", new JsonMediaTypeFormatter())
                    };
        }
    }
}
