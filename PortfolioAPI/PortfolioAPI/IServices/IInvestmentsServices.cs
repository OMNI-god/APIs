using PortfolioAPI.Models;

namespace PortfolioAPI.IServices
{
    public interface IInvestmentsServices
    {
        IEnumerable<Investment> GetInvestments();
        Investment GetInvestment(Guid id);
        void CreateInvestment(Investment investment);
        void DeleteInvestment(Guid id);
    }
}
