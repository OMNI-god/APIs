using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PortfolioAPI.Database;
using PortfolioAPI.IServices;
using PortfolioAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PortfolioAPI.Services
{
    public class InvestmentsServices : IInvestmentsServices
    {
        private readonly APIDB context;
        private CommonMethods commonMethods;
        public InvestmentsServices(APIDB context, CommonMethods commonMethods)
        {
            this.context = context;
            this.commonMethods = commonMethods;

        }

        public Investment CreateInvestment(Investment investment,string token)
        {
            string userid = commonMethods.getDataFromJwtToken(token);
            investment.Id = Guid.NewGuid();
            investment.UserId = new Guid(userid);
            context.investments.AddAsync(investment);
            context.SaveChanges();
            return investment;
        }

        public Investment DeleteInvestment(Guid id)
        {
            if (context.investments == null)
            {
                return null;
            }
            var investment = context.investments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (investment.Result == null)
            {
                return null;
            }
            context.investments.Remove(investment.Result);
            context.SaveChanges();
            return investment.Result;
        }

        public Investment GetInvestment(Guid id)
        {
            if (context.investments == null)
            {
                return null;
            }
            var investment = context.investments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (investment.Result == null)
            {
                return null;
            }
            return investment.Result;
        }

        public IEnumerable<Investment> GetInvestments(string token)
        {
            if (context.investments == null)
            {
                return null;
            }
            Guid userid = new Guid(commonMethods.getDataFromJwtToken(token));
            return context.investments.Where(x=>x.UserId==userid);
        }

        public bool AlreadyExists(Guid id)
        {
            if (context.investments == null)
            {
                return false;
            }
            return context.investments.Any(x=> x.Id==id);
        }

        public Object UpdataInvestment( Guid id, Investment updatedInvestment)
        {
            if (context.investments == null)
            {
                return null;
            }

            Investment oldInvestment = context.investments.FindAsync(id).Result;
            if (oldInvestment == null)
            {
                return null;
            }
            context.Entry(oldInvestment).State = EntityState.Detached;

            updatedInvestment.Id = id;
            var updated = context.investments.Update(updatedInvestment);

            List<string> fieldList = commonMethods.ModifiedDataList(oldInvestment,updatedInvestment,context);

            
            context.SaveChanges();
            return new
            {
                fields_updated = fieldList,
                before_update = oldInvestment,
                updated_on = DateTime.UtcNow.ToString("d/MM/yyyy hh:mm tt 'UTC'")
            };
        }
        

    }
}
