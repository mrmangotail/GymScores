using System;
using System.Linq;
using System.Collections.Generic;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Concrete
{
    public class EFMeetRepository : IMeetRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Meet> Meets
        {
            get { return context.Meets; }
        }

        public void SaveMeet(Meet meet)
        {
            if (meet.MeetID == 0)
            {
                context.Meets.Add(meet);
            }
            else
            {
                Meet dbEntry = context.Meets.Find(meet.MeetID);

                if (dbEntry != null)
                {
                    dbEntry.Location = meet.Location;
                    dbEntry.MeetDate = meet.MeetDate;
                    dbEntry.Name = meet.Name;
                }
            }

            context.SaveChanges();
        }

        public bool DeleteMeet(int meetID)
        {
            Meet dbEntry = context.Meets.Find(meetID);

            if (dbEntry != null && !ScoresExist(meetID))
            {
                context.Meets.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        private bool ScoresExist(int meetID)
        {
            var scoresForMeet = (from s in context.Scores
                                 where s.MeetID == meetID
                                 select s).ToList();

            if (scoresForMeet.Count > 0)
                return true;
            else
                return false;
        }

        public List<MeetScore> MeetScores(int meetID)
        {
            return (from s in context.Scores
                    from g in context.Gymnasts
                    where s.MeetID == meetID && s.GymnastID == g.GymnastID
                    orderby s.IsCompulsory, s.AllAround descending
                    select new MeetScore
                    {
                        ScoreID = s.ScoreID,
                        MeetID = s.MeetID,
                        GymnastID = g.GymnastID,
                        GymnastName = g.FirstName + " " + g.LastName,
                        
                        BarsOnesTens = s.BarsOnesTens,
                        BarsTenths = s.BarsTenths,
                        BarsHundredths = s.BarsHundredths,
                        BarsThousandths = s.BarsThousandths,
                        
                        BeamOnesTens = s.BeamOnesTens,
                        BeamTenths = s.BeamTenths,
                        BeamHundredths = s.BeamHundredths,
                        BeamThousandths = s.BeamThousandths,

                        FloorOnesTens = s.FloorOnesTens,
                        FloorTenths = s.FloorTenths,
                        FloorHundredths = s.FloorHundredths,
                        FloorThousandths = s.FloorThousandths,

                        VaultOnesTens = s.VaultOnesTens,
                        VaultTenths = s.VaultTenths,
                        VaultHundredths = s.VaultHundredths,
                        VaultThousandths = s.VaultThousandths,

                        AllAround = s.AllAround,
                        IsCompulsory = s.IsCompulsory
                    }).ToList();
        }
    }
}
