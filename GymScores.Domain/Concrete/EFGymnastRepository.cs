using System.Linq;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Concrete
{
    public class EFGymnastRepository : IGymnastRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Gymnast> Gymnasts
        {
            get { return context.Gymnasts; }
        }

        public void SaveGymnast(Gymnast gymnast)
        {
            if (gymnast.GymnastID == 0)
            {
                context.Gymnasts.Add(gymnast);
            }
            else
            {
                Gymnast dbEntry = context.Gymnasts.Find(gymnast.GymnastID);

                if (dbEntry != null)
                {
                    dbEntry.FirstName = gymnast.FirstName;
                    dbEntry.LastName = gymnast.LastName;
                    dbEntry.Age = gymnast.Age;
                    dbEntry.Level = gymnast.Level;
                }
            }

            context.SaveChanges();
        }

        public bool DeleteGymnast(int gymnastID)
        {
            Gymnast dbEntry = context.Gymnasts.Find(gymnastID);

            if (dbEntry != null && !ScoresExist(gymnastID))
            {
                context.Gymnasts.Remove(dbEntry);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        private bool ScoresExist(int gymnastID)
        {
            var scoresForGymnast = (from s in context.Scores
                                    where s.GymnastID == gymnastID
                                    select s).ToList();

            if (scoresForGymnast.Count > 0)
                return true;
            else
                return false;
        }
    }
}