using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (TempData.Count != 0)
            {
                ApplicationDbContext Context = new ApplicationDbContext();
                ViewBag.CollageJSON = Context.Collages.Find(TempData["CollageID"]).collageInfo;
                ViewBag.CollageID = TempData["CollageID"];
                if (Context.Users.Find(TempData["UserID"]).UserName != User.Identity.Name)
                {
                    ViewBag.NonModify = false;
                }
            }
            return View();
        }

        public ActionResult SaveCollage(string userID, string stringedJSON, int collageID)
        {
            ApplicationDbContext Context = new ApplicationDbContext();
            ApplicationUser User = new ApplicationUser();
            foreach (var i in Context.Users)
            {
                if (i.UserName == userID)
                    User = i;
            }
            Collages newCollage = new Collages();
            newCollage.userID = User.Id;
            newCollage.collageInfo = stringedJSON;
            if (collageID == 0)
            {
                Context.Collages.Add(newCollage);
                Context.Entry(newCollage).State = System.Data.Entity.EntityState.Added;
            }
            else
            {
                Context.Collages.Find(collageID).collageInfo = stringedJSON;
                
            }
            Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ViewCollage(int CollageID, string UserID)
        {
            TempData["CollageID"] = CollageID;
            TempData["UserID"] = UserID;
            return RedirectToAction("Index");
        }
    }
}