namespace GymScores.Domain.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
        void Logout();
    }
}
