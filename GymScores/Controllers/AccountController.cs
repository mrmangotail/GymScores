using System.Web.Mvc;
using GymScores.Domain.Abstract;
using GymScores.ViewModels;

namespace GymScores.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("List", "Meet"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            authProvider.Logout();
            return RedirectToAction("List", "Meet");
        }
    }
}