using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
       

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index()
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            System.Collections.Generic.IEnumerable<ApplicationUser> tmp = DB.Users.AsEnumerable();        
            return View(tmp);
        }

        public async Task<ActionResult> Ban(string id)
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            DB.Users.Find(id).LockoutEnabled = !DB.Users.Find(id).LockoutEnabled;
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            ApplicationUser user = DB.Users.Find(id);
            DB.Entry(user).State = System.Data.Entity.EntityState.Deleted; 
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
        //

        public async Task<ActionResult> Collages(string UserID)
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            System.Collections.Generic.IEnumerable<Collages> tmp = DB.Collages.AsEnumerable();
            ViewBag.UserID = UserID;
            string CurrentUserID = "";
            foreach (var i in DB.Users)
            {
                if (i.UserName == User.Identity.Name)
                    CurrentUserID = i.Id;
            }
            ViewBag.CurrentUserID = CurrentUserID;
            return View(tmp);
        }

        public async Task<ActionResult> DeleteCollage(int CollageID)
        {
            ApplicationDbContext DB = new ApplicationDbContext();
            foreach(var i in DB.Collages)
            {
                if (i.CollagesID == CollageID)
                {
                    Collages Collage = DB.Collages.Find(CollageID);
                    DB.Entry(Collage).State = System.Data.Entity.EntityState.Deleted;
                    
                }
            }
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}