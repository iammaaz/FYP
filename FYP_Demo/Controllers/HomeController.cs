using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Demo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Taqreeb_FYPEntities context = new Taqreeb_FYPEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetVenues()
        {
            return View();
        }

        public ActionResult AddImage()
        {
            ImageDemo img = new ImageDemo();
            return View(img);
        }
        [HttpPost]
        public ActionResult AddImage(ImageDemo model1, HttpPostedFileBase image1)
        {
            if (image1!=null)
            {
                model1.HallImage = new byte[image1.ContentLength];
                image1.InputStream.Read(model1.HallImage, 0, image1.ContentLength);
            }
            context.ImageDemoes.Add(model1);
            context.SaveChanges();
            return View(model1);
        }

    }
}
