using System;
using System.Linq;
using System.Web.Mvc;
using GymScores.Domain.Abstract;
using GymScores.Domain.Concrete;
using GymScores.Domain.Entities;
using GymScores.ViewModels;

namespace GymScores.Controllers
{
    [Authorize]
    public class ScoreController : Controller
    {
        private IScoreRepository repository;

        public ScoreController(IScoreRepository scoreRepository)
        {
            repository = scoreRepository;

        }

        [HttpPost]
        public ViewResult Add(int meetID)
        {
            var scoresViewModel = new ScoreViewModel
            {
                Score = new Score
                {
                    MeetID = meetID
                }
            };
            
            return View(scoresViewModel);
        }

        [HttpPost]
        public ActionResult Save(Score score)
        {
            var scoreViewModel = new ScoreViewModel
            {
                Score = score
            };

            if (IsValidScore(score))
            {
                bool isSaved = repository.SaveScore(score);

                if (isSaved)
                {
                    return RedirectToAction("Details", "Meet", new { meetID = score.MeetID });   
                }
                else
                {
                    TempData["message"] = String.Format("Unable to save score. Check for duplicates.");
                    return View("Add", scoreViewModel);
                }
            }
            else
            {
                return View("Add", scoreViewModel);
            }
        }

        public ViewResult Edit(int scoreID)
        {
            var viewModel = new ScoreViewModel();
            var score = repository.Scores.FirstOrDefault(m => m.ScoreID == scoreID);
            viewModel.Score = score;

            if (score != null)
            {
                var gymnast = new EFGymnastRepository().GetGymnast(score.GymnastID);
                viewModel.GymnastName = gymnast.FirstName + " " + gymnast.LastName;
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ScoreViewModel scoreViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isSaved = repository.SaveScore(scoreViewModel.Score);

                if (isSaved)
                {
                    return RedirectToAction("Details", "Meet", new { meetID = scoreViewModel.Score.MeetID});
                }
                else
                {
                    TempData["message"] = "Unable to save score. Check for duplicates.";
                    return View("Edit", scoreViewModel);
                }
            }
            else
            {
                TempData["message"] = "Error saving score.";
                return View("Edit", scoreViewModel);
            }
        }

        private bool IsValidScore(Score score)
        {
            var barsString = String.Format("{0}.{1}{2}{3}", score.BarsOnesTens, score.BarsTenths, score.BarsHundredths, score.BarsThousandths);
            var barScore = decimal.Parse(barsString);

            var beamString = String.Format("{0}.{1}{2}{3}", score.BeamOnesTens, score.BeamTenths, score.BeamHundredths, score.BeamThousandths);
            var beamScore = decimal.Parse(beamString);

            var floorString = String.Format("{0}.{1}{2}{3}", score.FloorOnesTens, score.FloorTenths, score.FloorHundredths, score.FloorThousandths);
            var floorScore = decimal.Parse(floorString);

            var vaultString = String.Format("{0}.{1}{2}{3}", score.VaultOnesTens, score.VaultTenths, score.VaultHundredths, score.VaultThousandths);
            var vaultScore = decimal.Parse(vaultString);

            return (barScore >= 0 &&
                    beamScore >= 0 &&
                    floorScore >= 0 &&
                    vaultScore >= 0 &&
                    score.GymnastID > 0 &&
                    score.MeetID > 0);
        }

        [HttpPost]
        public ActionResult Delete(int scoreID)
        {
            Score deletedScore = repository.DeleteScore(scoreID);

            if (deletedScore == null)
            {
                TempData["message"] = "Unable to delete score.";
            }

            return RedirectToAction("Details", "Meet", new { meetID = deletedScore.MeetID });
        }
    }
}
