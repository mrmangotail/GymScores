using System.Linq;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Abstract
{
    public interface IScoreRepository
    {
        IQueryable<Score> Scores{ get; }

        bool SaveScore(Score score);

        Score DeleteScore(int scoreID);
    }
}