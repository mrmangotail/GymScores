using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymScores.Domain.Entities
{
    public class Gymnast
    {
        [HiddenInput(DisplayValue = false)]
        public int GymnastID { get; set; }

        [Required(ErrorMessage = "First name required")]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Level required")]
        [MaxLength(10)]
        public string Level { get; set; }
    }
}