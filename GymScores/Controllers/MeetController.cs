using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;
using GymScores.ViewModels;

namespace GymScores.Controllers
{
    public class MeetController : Controller
    {
        private IMeetRepository repository;
        
        public MeetController(IMeetRepository meetRepository)
        {
            repository = meetRepository;
        }

        [Authorize]
        public ViewResult List()
        {
            return View(repository.Meets);
        }

        [Authorize]
        [HttpGet]
        public ViewResult Add()
        {
            var meet = new Meet
            {
                MeetDate = DateTime.Now
            };

            return View(meet);
        }

        [Authorize]
        [HttpPost]
        public ViewResult Add(Meet meet)
        {
            if (ModelState.IsValid)
            {
                repository.SaveMeet(meet);
                
                var meetScores = new MeetScoresViewModel
                {
                    Meet = meet,
                    MeetScores = new List<MeetScore>()
                };

                return View("Details", meetScores);
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ViewResult Edit(int meetID)
        {
            Meet meet = repository.Meets.FirstOrDefault(m => m.MeetID == meetID);
            return View(meet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Meet meet)
        {
            if (ModelState.IsValid)
            {
                repository.SaveMeet(meet);
                return RedirectToAction("List");
            }
            else
            {
                return View(meet);
            }
        }

        [Authorize]
        public ViewResult Details(int meetID)
        {
            var meetScores = new MeetScoresViewModel
            {
                Meet = repository.Meets.FirstOrDefault(m => m.MeetID == meetID),
                MeetScores = repository.MeetScores(meetID)
            };

            return View(meetScores);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int meetID)
        {
            if (!repository.DeleteMeet(meetID))
            {
                TempData["message"] = "Unable to delete meet with scores. Delete scores first, then meet.";
            }

            return RedirectToAction("List");
        }

        public ViewResult Results(int meetID)
        {
            var meetScores = new MeetScoresViewModel
            {
                Meet = repository.Meets.FirstOrDefault(m => m.MeetID == meetID),
                MeetScores = repository.MeetScores(meetID)
            };

            return View(meetScores);
        }
    }
}