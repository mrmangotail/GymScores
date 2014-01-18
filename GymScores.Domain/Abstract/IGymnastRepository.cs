using System.Linq;
using GymScores.Domain.Entities;

namespace GymScores.Domain.Abstract
{
    public interface IGymnastRepository
    {
        IQueryable<Gymnast> Gymnasts { get; }
        
        void SaveGymnast(Gymnast gymnast);

        bool DeleteGymnast(int gymnastID);
    }
}