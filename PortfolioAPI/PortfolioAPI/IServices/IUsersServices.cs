using PortfolioAPI.Models;

namespace PortfolioAPI.IServices
{
    public interface IUsersServices
    {
        Object Register(User user);
        string Authenticate(string[] param);
        Object DeleteUser(Guid id);
        Object UpdateUser(Guid id, User user);
        Object ResetPassword(Object credentials,string token);
    }
}
