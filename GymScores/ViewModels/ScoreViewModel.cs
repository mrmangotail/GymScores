using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using GymScores.Domain.Entities;
using GymScores.Domain.Concrete;

namespace GymScores.ViewModels
{
    public class ScoreViewModel
    {
        public Score Score { get; set; }
        public int GymnastID { get; set; }
        
        // bars
        public IEnumerable<SelectListItem> BarsOnesTensList
        {
            get { return CreateNumericList(13); }
        }

        public IEnumerable<SelectListItem> BarsTenthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> BarsHundredthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> BarsThousandthsList
        {
            get { return CreateNumericList(10); }
        }

        // beam
        public IEnumerable<SelectListItem> BeamOnesTensList
        {
            get { return CreateNumericList(13); }
        }

        public IEnumerable<SelectListItem> BeamTenthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> BeamHundredthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> BeamThousandthsList
        {
            get { return CreateNumericList(10); }
        }

        // floor
        public IEnumerable<SelectListItem> FloorOnesTensList
        {
            get { return CreateNumericList(13); }
        }

        public IEnumerable<SelectListItem> FloorTenthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> FloorHundredthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> FloorThousandthsList
        {
            get { return CreateNumericList(10); }
        }

        // vault
        public IEnumerable<SelectListItem> VaultOnesTensList
        {
            get { return CreateNumericList(13); }
        }

        public IEnumerable<SelectListItem> VaultTenthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> VaultHundredthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> VaultThousandthsList
        {
            get { return CreateNumericList(10); }
        }

        public IEnumerable<SelectListItem> Gymnasts
        {
            get 
            {
                var gymnasts = new EFGymnastRepository().Gymnasts.ToList();
                var gymnastList = from g in gymnasts
                                  select new SelectListItem
                                  {
                                      Value = g.GymnastID.ToString(),
                                      Text = g.FirstName + " " + g.LastName
                                  };

                return gymnastList; 
            }
        }

        private IEnumerable<SelectListItem> CreateNumericList(int endValue)
        {
            var numericList = new List<SelectListItem>();
            var isSelected = false;

            for (int i = 0; i <= endValue; i++)
            {
                if (i == 0)
                    isSelected = true;

                numericList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = isSelected });
            }

            return numericList;
        }
    }
}