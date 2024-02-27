using PortfolioAPI.Models;

namespace PortfolioAPI.IServices
{
    public interface IInvestmentsServices
    {
        IEnumerable<Investment> GetInvestments(string token);
        Investment GetInvestment(Guid id);
        Investment CreateInvestment(Investment investment,string token);
        Investment DeleteInvestment(Guid id);
        Investment UpdataInvestment(Guid id, Investment updatedInvestment);
        bool AlreadyExists(Guid id);
    }
}
