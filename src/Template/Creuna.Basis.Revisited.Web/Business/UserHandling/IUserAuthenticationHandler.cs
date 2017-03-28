namespace Creuna.Basis.Revisited.Web.Business.UserHandling
{
    public interface IUserAuthenticationHandler
    {
        bool Login(string username, string password, bool persistLogin);
        void Logout();
    }
}