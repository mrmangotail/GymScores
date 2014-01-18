using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymScores.Domain.Entities
{
    public class MeetScore
    {
        [HiddenInput(DisplayValue = false)]
        public int ScoreID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MeetID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int GymnastID { get; set; }

        [Required(ErrorMessage = "First name required")]
        [MaxLength(35)]
        public string GymnastName { get; set; }

        public int BarsOnesTens { get; set; }
        public int BarsTenths { get; set; }
        public int BarsHundredths { get; set; }
        public int BarsThousandths { get; set; }

        public int BeamOnesTens { get; set; }
        public int BeamTenths { get; set; }
        public int BeamHundredths { get; set; }
        public int BeamThousandths { get; set; }

        public int FloorOnesTens { get; set; }
        public int FloorTenths { get; set; }
        public int FloorHundredths { get; set; }
        public int FloorThousandths { get; set; }

        public int VaultOnesTens { get; set; }
        public int VaultTenths { get; set; }
        public int VaultHundredths { get; set; }
        public int VaultThousandths { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal AllAround { get; set; }

        public bool IsCompulsory { get; set; }
    }
}