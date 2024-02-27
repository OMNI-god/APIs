using PortfolioAPI.Models;

namespace PortfolioAPI.IServices
{
    public interface IUsersServices
    {
        List<Object> Register(User user);
        string Authenticate(string[] param);
        Object DeleteUser(Guid id);
        Object UpdateUser(Guid id, User user);
        Object ResetPassword(Guid id,User user,string token);
    }
}
