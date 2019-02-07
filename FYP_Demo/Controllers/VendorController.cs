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

        [HttpGet]
        public ActionResult AddVenue(int id = 0)
        {
            
            HallInfo Hall = new HallInfo();

            if (id != 0) //For Update:
            {
                //hall = context.HallInfoes.Where(x => x.HallInfoID == id).FirstOrDefault();
                Hall = context.HallInfoes.Find(id);

                //var HallTypeIDs = from HallType in context.HallTypes
                //            from HallInfo in HallType.HallInfoes
                //            where HallInfo.HallInfoID == id
                //            select HallType.HallTypeID;
                //Hall.HallTypeSelectedIDs = HallTypeIDs.ToList();

                PopulatingDropdownsForUpdate(Hall, id);
            }

                Hall.HallTypes = context.HallTypes.ToList();
                Hall.HallFacilities = context.HallFacilities.ToList();
                Hall.EventTypes = context.EventTypes.ToList();

            Hall.HallActivities = context.HallActivities.ToList();


            return View(Hall);
        }
        [HttpPost]
        public ActionResult AddVenue(HallInfo hall)
        {
            //Get Vendor ID
            hall.VendorInfoID = User.Identity.GetUserId();
            //Insert Dropdowns Data
            InsertDropdownSelectedData(hall);
            
            if (hall.HallInfoID == 0)
            {
                context.HallInfoes.Add(hall);

            }
            else
            {
                //context.Entry(hall).State = EntityState.Detached;
                //context.ChangeTracker.DetectChanges();
                context.Entry(hall).State = EntityState.Modified;
                //context.HallInfoes.Attach(hall);
                //context.ChangeTracker.DetectChanges();
            }

            context.SaveChanges();
            
           
            ViewBag.msg = "Venue added successfully";
            //return View(hall);
            return RedirectToAction("MyListing");
        }

        public ActionResult MyListing()
        {
            //HallInfo hall = new HallInfo();
            //hall = context.HallInfoes.Find(id);
            ////return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
            //return View(hall);
            var userID = User.Identity.GetUserId();
            var  halls = from HallInfo in context.HallInfoes
                            where HallInfo.VendorInfoID.Equals(userID)
                            select HallInfo;
            
            return View(halls);
        }

        public ActionResult VenueDetails(int id)
        {
            HallInfo hall = new HallInfo();
            
                hall = context.HallInfoes.Find(id);
                //return View(from HallInfo in context.HallInfoes where HallInfo.HallInfoID == id select HallInfo);
                return View(hall);
            
               
        }

        [HttpGet]
        public ActionResult DeleteHall(int id)
        {
            context.HallInfoes.Remove(context.HallInfoes.Find(id));
                context.SaveChanges();
                return RedirectToAction("MyListing");
        }

       

        public void InsertDropdownSelectedData(HallInfo hall)
        {
                if (hall.HallTypeSelectedIDs != null)
                {
                    foreach (var item in hall.HallTypeSelectedIDs)
                    {
                        hall.HallTypes.Add(context.HallTypes.Find(item));
                    }
                }
                if (hall.HallEventTypeSelectedIDs != null)
                {
                    foreach (var item in hall.HallEventTypeSelectedIDs)
                    {
                        hall.EventTypes.Add(context.EventTypes.Find(item));
                    }
                }
                if (hall.HallActivitiesSelectedIDs != null)
                {
                    foreach (var item in hall.HallActivitiesSelectedIDs)
                    {
                        hall.HallActivities.Add(context.HallActivities.Find(item));
                    }
                }
                if (hall.HallFacilitySelectedIDs != null)
                {
                    foreach (var item in hall.HallFacilitySelectedIDs)
                    {
                        hall.HallFacilities.Add(context.HallFacilities.Find(item));
                    }
                }
            
        }
        public void PopulatingDropdownsForUpdate(HallInfo Hall, int id)
        {
            
                //Getting hall Types
                Hall.HallTypeSelectedIDs = context.HallTypes
                    .Where(ht => ht.HallInfoes
                    .Any(hi => hi.HallInfoID == id))
                    .Select(i => i.HallTypeID).ToList();

                //Getting hall Activities
                Hall.HallActivitiesSelectedIDs = context.HallActivities
                    .Where(ht => ht.HallInfoes
                    .Any(hi => hi.HallInfoID == id))
                    .Select(i => i.HallActivityID).ToList();

                //getting Hall Events
                Hall.HallEventTypeSelectedIDs = context.EventTypes
                    .Where(ht => ht.HallInfoes
                    .Any(hi => hi.HallInfoID == id))
                    .Select(i => i.EventTypeID).ToList();


                Hall.HallFacilitySelectedIDs = context.HallFacilities
                    .Where(ht => ht.HallInfoes
                    .Any(hi => hi.HallInfoID == id))
                    .Select(i => i.HallFacilityID).ToList();
            
        }
    }
}