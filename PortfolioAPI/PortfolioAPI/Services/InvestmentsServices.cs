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
        public InvestmentsServices(APIDB context)
        {
            this.context = context;
        }

        public Investment CreateInvestment(Investment investment,string token)
        {
            string userid = getDataFromJwtToken(token);
            investment.Id = Guid.NewGuid();
            investment.UserId = new Guid(userid);
            context.investments.AddAsync(investment);
            context.SaveChanges();
            return investment;
        }

        private string getDataFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token = token.Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries)[1];
            if (tokenHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
                Claim userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim != null)
                {
                    return userIdClaim.Value.ToString();
                }
            }
            return "";
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
            Guid userid = new Guid(getDataFromJwtToken(token));
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

        public Investment UpdataInvestment( Guid id, Investment updatedInvestment)
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
            context.investments.Update(updatedInvestment);
            context.SaveChanges();

            return oldInvestment;
        }

    }
}
