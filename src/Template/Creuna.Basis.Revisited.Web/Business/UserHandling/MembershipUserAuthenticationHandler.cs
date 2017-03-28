using System.Web.Security;

namespace Creuna.Basis.Revisited.Web.Business.UserHandling
{
    /// <summary>
    /// .Net Membership implementation of user authentication.
    /// </summary>
    class MembershipUserAuthenticationHandler : IUserAuthenticationHandler
    {
        public bool Login(string username, string password, bool persistLogin)
        {
            var validCredentials = Membership.ValidateUser(username, password);
            if (validCredentials)
                FormsAuthentication.SetAuthCookie(username, persistLogin);

            return validCredentials;
        }

        public void Logout()
            => FormsAuthentication.SignOut();
    }
}