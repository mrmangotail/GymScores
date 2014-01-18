using System.Web.Mvc;

namespace GymScores.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("List", "Meet");
        }
    }
}