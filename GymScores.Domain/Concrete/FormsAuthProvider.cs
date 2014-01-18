using System.Web.Security;
using GymScores.Domain.Abstract;

namespace GymScores.Domain.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            var result = FormsAuthentication.Authenticate(username, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}