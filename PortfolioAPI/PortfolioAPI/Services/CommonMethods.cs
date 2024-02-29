using PortfolioAPI.Database;
using PortfolioAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PortfolioAPI.Services
{
    public class CommonMethods
    {
        public CommonMethods()
        {
            
        }
        public List<string> ModifiedDataList<T>(T orginalData,T updatedData,APIDB context)
        {
            List<string> fieldList = new List<string>();
            dynamic properties;
            properties = context.Entry(updatedData).Properties;
            
            foreach (var property in properties)
            {
                Console.WriteLine(property.Metadata.Name);
                if (property.OriginalValue == null) continue;
                string orginalVal = context.Entry(orginalData).Properties.FirstOrDefault(x => x.Metadata.Name == property.Metadata.Name).OriginalValue.ToString();
                string updatedVal = property.CurrentValue.ToString();
                if (orginalVal != updatedVal)
                {
                    fieldList.Add(property.Metadata.Name);
                }
            }
            return fieldList;
        }
        public string getDataFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token = token.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
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
    }
}
