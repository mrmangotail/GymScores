using System.Linq;
using System.Collections.Generic;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Abstract
{
    public interface IMeetRepository
    {
        IQueryable<Meet> Meets { get; }
        
        void SaveMeet(Meet meet);

        bool DeleteMeet(int meetID);

        List<MeetScore> MeetScores(int meetID);
    }
}