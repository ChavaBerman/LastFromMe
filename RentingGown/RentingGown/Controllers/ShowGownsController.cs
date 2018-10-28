using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentingGown.Models;
using System.Net;

namespace RentingGown.Controllers
{
    public class ShowGownsController : Controller
    {
        private RentingGownEntities db = new RentingGownEntities();

        // GET: ShowGowns
        public ActionResult ShowGowns(List<Gowns> CurrentGowns)
        {
            return View(CurrentGowns);
        }
  
    }
}