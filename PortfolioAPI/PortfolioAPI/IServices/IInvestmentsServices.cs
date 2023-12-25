using PortfolioAPI.Models;

namespace PortfolioAPI.IServices
{
    public interface IInvestmentsServices
    {
        IEnumerable<Investment> GetInvestments();
        Investment GetInvestment(int id);

    }
}
