using System;
using System.Linq;
using System.Web.Mvc;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;

namespace GymScores.Controllers
{
    [Authorize]
    public class GymnastController : Controller
    {
        private IGymnastRepository repository;

        public GymnastController(IGymnastRepository gymnastRepository)
        {
            repository = gymnastRepository;
        }

        public ViewResult List()
        {
            return View(repository.Gymnasts);
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Add(Gymnast gymnast)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGymnast(gymnast);
                return View("List", repository.Gymnasts);
            }
            else
            {
                return View();
            }
        }

        public ViewResult Edit(int gymnastID)
        {
            Gymnast gymnast = repository.Gymnasts.FirstOrDefault(m => m.GymnastID == gymnastID);
            return View(gymnast);
        }

        [HttpPost]
        public ActionResult Edit(Gymnast gymnast)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGymnast(gymnast);
                return RedirectToAction("List", "Gymnast");
            }
            else
            {
                return View(gymnast);
            }
        }

        public ViewResult Details(int gymnastID)
        {
            Gymnast gymnast = repository.Gymnasts.FirstOrDefault(m => m.GymnastID == gymnastID);
            return View(gymnast);
        }

        [HttpPost]
        public ActionResult Delete(int gymnastID)
        {
            if (!repository.DeleteGymnast(gymnastID))
            {
                TempData["message"] = String.Format("Unable to delete gymnast. Delete scores associated with gymnast first.");
            }

            return RedirectToAction("List");
        }
    }
}