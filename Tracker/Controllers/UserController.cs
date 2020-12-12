using System.Linq;
using System.Web.Mvc;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class UserController : Controller
    {
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Create(User user)
        {
            return View();
        }
    }
}
