using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP_Demo.Models;
using Microsoft.AspNet.Identity;


namespace FYP_Demo.Controllers
{
    public class VendorController : Controller
    {

        Taqreeb_FYPEntities context = new Taqreeb_FYPEntities();
        // GET: Vendor
        public ActionResult VendorPanel()
        {
            return View();
        }
        //Car Add and Update
        public ActionResult AddCar(int id = 0)
        {
            CarInfo car = new CarInfo();
            if (id != 0)
            {
               car = context.CarInfoes.Find(id);
                return View(car);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddCar(CarInfo car)
        {
            //Get Vendor ID
            car.VendorInfoID = User.Identity.GetUserId();
            

            if (car.CarID == 0)
            {
                context.CarInfoes.Add(car);

            }
            else
            {
                context.Entry(car).State = EntityState.Modified;
            }

            context.SaveChanges();

            //ViewBag.msg = "Venue added successfully";
            //return View(hall);
            return RedirectToAction("AddCar", new { id = 0 });
        }

        public ActionResult MyCars()
        {
            //HallInfo hall = new HallInfo();
            //hall = context.HallInfoes.Find(id);
            ////return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
            //return View(hall);
            var userID = User.Identity.GetUserId();
            return View(from CarInfo in context.CarInfoes
                        where CarInfo.VendorInfoID.Equals(userID)
                        select CarInfo);
        }

        public ActionResult CarDetails(int id)
        {
            CarInfo car = new CarInfo();
            car = context.CarInfoes.Find(id);
            //return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
            return View(car);
        }

        public ActionResult AddVenue(int id = 0)
        {
            VenueViewModel viewModel = new VenueViewModel();
            viewModel.Hall = new HallInfo();
            //HallInfo hall = new HallInfo();
            
            if (id != 0)
            {
                //hall = context.HallInfoes.Where(x => x.HallInfoID == id).FirstOrDefault();
                viewModel.Hall = context.HallInfoes.Find(id);

                //var query = from ht in context.HallTypes
                //            from hi in ht.HallInfoes
                //            where hi.HallInfoID == id
                //            select ht.HallTypeID;

                            //int[] arr = new int[] { 3, 4 };
                            //hall.HallTypeSelectedIDs = arr;

                            //var query = context.HallTypes.Where(ht => ht.HallInfoes.Any(hi => hi.HallInfoID == id)).Select(i => i.HallTypeID).ToList();
                            //var query = hall.HallTypes.Where(h => h.HallInfoes == hall).Select(i => i.HallTypeID).ToList();

                            //var query = from ht in context.HallTypes
                            //            from hi in ht.HallInfoes
                            //            where hi.HallInfoID == id
                            //            select new { IdList = ht.HallTypeID };

            }

            viewModel.Hall.HallTypes = context.HallTypes.ToList();
            viewModel.Hall.HallFacilities = context.HallFacilities.ToList();
            viewModel.Hall.EventTypes = context.EventTypes.ToList();
            viewModel.Hall.HallActivities = context.HallActivities.ToList();

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AddVenue(VenueViewModel hall)
        {
            //Get Vendor ID
            hall.Hall.VendorInfoID = User.Identity.GetUserId();
            //Insert Dropdowns Data
            InsertDropdownSelectedData(hall);

            if (hall.Hall.HallInfoID == 0)
            {
                context.HallInfoes.Add(hall.Hall);
                
            }
            else
            {
                context.Entry(hall.Hall).State = EntityState.Modified;
            }

            context.SaveChanges();

            ViewBag.msg = "Venue added successfully";
            //return View(hall);
            return RedirectToAction("AddVenue", new { id = 0 });
        }

        public ActionResult MyListing()
        {
            //HallInfo hall = new HallInfo();
            //hall = context.HallInfoes.Find(id);
            ////return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
            //return View(hall);
            var userID = User.Identity.GetUserId();
            return View(from HallInfo in context.HallInfoes
                        where HallInfo.VendorInfoID.Equals(userID) select HallInfo);
        }

        public ActionResult VenueDetails(int id)
        {
            HallInfo hall = new HallInfo(); 
                hall = context.HallInfoes.Find(id);
            //return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
            return View(hall);
        }

        [HttpPost]
        public ActionResult DeleteVenue(int id, HallInfo hall)
        {
                context.HallInfoes.Remove(context.HallInfoes.Find(id));
                context.SaveChanges();
                return RedirectToAction("MyListing");
        }

       

        public void InsertDropdownSelectedData(VenueViewModel hall)
        {
            if (hall.HallTypeSelectedIDs != null)
            {
                foreach (var item in hall.HallTypeSelectedIDs)
                {
                    hall.Hall.HallTypes.Add(context.HallTypes.Find(item));
                }
            }
            if (hall.HallEventTypeSelectedIDs != null)
            {
                foreach (var item in hall.HallEventTypeSelectedIDs)
                {
                    hall.Hall.EventTypes.Add(context.EventTypes.Find(item));
                }
            }
            if (hall.HallActivitiesSelectedIDs != null)
            {
                foreach (var item in hall.HallActivitiesSelectedIDs)
                {
                    hall.Hall.HallActivities.Add(context.HallActivities.Find(item));
                }
            }
            if (hall.HallFacilitySelectedIDs != null)
            {
                foreach (var item in hall.HallFacilitySelectedIDs)
                {
                    hall.Hall.HallFacilities.Add(context.HallFacilities.Find(item));
                }
            }
        }
    }
}