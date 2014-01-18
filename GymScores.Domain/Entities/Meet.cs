using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymScores.Domain.Entities
{
    public class Meet
    {
        [HiddenInput(DisplayValue = false)]
        public int MeetID { get; set; }

        [Required(ErrorMessage = "Meet name required")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location required")]
        [MaxLength(50)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Date required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MeetDate { get; set; }
    }
}