using System;
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
            try
            {
                var validCredentials = Membership.ValidateUser(username, password);
                if (validCredentials)
                    FormsAuthentication.SetAuthCookie(username, persistLogin);

                return validCredentials;
            }
            catch (SystemException)
            {
                // Happens when trying to log in with a user that doesn't exist in AD
                return false;
            }
        }

        public void Logout()
            => FormsAuthentication.SignOut();
    }
}