using System.Collections.Generic;
using GymScores.Domain.Entities;

namespace GymScores.ViewModels
{
    public class MeetScoresViewModel
    {
        public Meet Meet { get; set; }
        public List<MeetScore> MeetScores { get; set; }
    }
}