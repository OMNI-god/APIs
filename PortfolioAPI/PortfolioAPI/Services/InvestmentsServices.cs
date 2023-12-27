using PortfolioAPI.Database;
using PortfolioAPI.IServices;
using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public class InvestmentsServices : IInvestmentsServices
    {
        private readonly APIDB context;
        public InvestmentsServices(APIDB context)
        {
            this.context = context;
        }
        public void DeleteInvestment(Guid id)
        {
            throw new NotImplementedException();
        }

        public Investment GetInvestment(int id)
        {
            throw new NotImplementedException();
        }

        public Investment GetInvestment(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Investment> GetInvestments()
        {
            throw new NotImplementedException();
        }

        public void CreateInvestment(Investment investment)
        {
            throw new NotImplementedException();
        }
    }
}
