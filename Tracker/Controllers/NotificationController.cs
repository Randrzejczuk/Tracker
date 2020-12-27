using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;
using System.Data.Entity;

namespace Tracker.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        
        public ActionResult Create(int issueId)
        {
            var context = new TrackerDbContext();
            return View();
        }
        public ActionResult List(int issueId)
        {
            var context = new TrackerDbContext();
            Issue issue = context.Issues.Where(x => x.Id == issueId).Include(x=>x.Notifications).FirstOrDefault();
            return View(issue);
        }
    }
}