using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Demo.Controllers
{
    public class ListyHomeController : Controller
    {
        Taqreeb_FYPEntities context = new Taqreeb_FYPEntities();
        // GET: ListyHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExploreVenues()
        {
            return View(from HallInfo in context.HallInfoes.Take(10)
                        select HallInfo);
            
        }
    }
}