using System.Linq;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Abstract
{
    public interface IGymnastRepository
    {
        IQueryable<Gymnast> Gymnasts { get; }

        Gymnast GetGymnast(int gymnastID);

        void SaveGymnast(Gymnast gymnast);

        bool DeleteGymnast(int gymnastID);
    }
}