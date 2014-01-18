using System;
using System.Linq;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Concrete
{
    public class EFScoreRepository : IScoreRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Score> Scores
        {
            get { return context.Scores; }
        }

        public bool SaveScore(Score score)
        {   
            if (score.ScoreID == 0)
            {
                if (!IsDuplicateScore(score))
                {
                    score.AllAround = CalculateAllAround(score); 
                    context.Scores.Add(score);
                    context.SaveChanges();
                    return true;
                }
            }
            else
            {
                Score dbEntry = context.Scores.Find(score.ScoreID);

                if (dbEntry != null)
                {
                    dbEntry.GymnastID = score.GymnastID;
                    dbEntry.MeetID = score.MeetID;

                    dbEntry.BarsOnesTens = score.BarsOnesTens;
                    dbEntry.BarsTenths = score.BarsTenths;
                    dbEntry.BarsHundredths = score.BarsHundredths;
                    dbEntry.BarsThousandths = score.BarsThousandths;

                    dbEntry.BeamOnesTens = score.BeamOnesTens;
                    dbEntry.BeamTenths = score.BeamTenths;
                    dbEntry.BeamHundredths = score.BeamHundredths;
                    dbEntry.BeamThousandths = score.BeamThousandths;

                    dbEntry.FloorOnesTens = score.FloorOnesTens;
                    dbEntry.FloorTenths = score.FloorTenths;
                    dbEntry.FloorHundredths = score.FloorHundredths;
                    dbEntry.FloorThousandths = score.FloorThousandths;

                    dbEntry.VaultOnesTens = score.VaultOnesTens;
                    dbEntry.VaultTenths = score.VaultTenths;
                    dbEntry.VaultHundredths = score.VaultHundredths;
                    dbEntry.VaultThousandths = score.VaultThousandths;
                    
                    dbEntry.AllAround = CalculateAllAround(score);
                    dbEntry.IsCompulsory = score.IsCompulsory;
                }

                context.SaveChanges();
                return true;
            }

            return false;
        }

        private decimal CalculateAllAround(Score score)
        {
            var barsString = String.Format("{0}.{1}{2}{3}", score.BarsOnesTens, score.BarsTenths, score.BarsHundredths, score.BarsThousandths);
            var barScore = decimal.Parse(barsString);

            var beamString = String.Format("{0}.{1}{2}{3}", score.BeamOnesTens, score.BeamTenths, score.BeamHundredths, score.BeamThousandths);
            var beamScore = decimal.Parse(beamString);

            var floorString = String.Format("{0}.{1}{2}{3}", score.FloorOnesTens, score.FloorTenths, score.FloorHundredths, score.FloorThousandths);
            var floorScore = decimal.Parse(floorString);

            var vaultString = String.Format("{0}.{1}{2}{3}", score.VaultOnesTens, score.VaultTenths, score.VaultHundredths, score.VaultThousandths);
            var vaultScore = decimal.Parse(vaultString);

            return barScore +
                   beamScore + 
                   floorScore + 
                   vaultScore;
        }

        private bool IsDuplicateScore(Score score)
        {
            var allScores = (from s in context.Scores
                             where s.MeetID == score.MeetID
                             && s.GymnastID == score.GymnastID
                             && s.IsCompulsory == score.IsCompulsory
                             select s).ToList();

            if (allScores.Count <= 0)
                return false;
            else
                return true;
        }

        public Score DeleteScore(int scoreID)
        {
            Score dbEntry = context.Scores.Find(scoreID);

            if (dbEntry != null)
            {
                context.Scores.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
