using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioAPI.Database;
using PortfolioAPI.IServices;
using PortfolioAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PortfolioAPI.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly APIDB _context;
        private IConfiguration config;
        private CommonMethods commonMethods;
        public UsersServices(APIDB context,IConfiguration config, CommonMethods commonMethods)
        {
            _context = context;
            this.config = config;
            this.commonMethods = commonMethods;

        }
        public string Authenticate(string[] param)
        {
            var data= _context.users.FirstOrDefaultAsync(x => x.EmailId == param[0] && x.Password == GeneratePasswordHash(param[1]));
            if (data.Result == null)
            {
                return "not found";
            }
            return GenerateToken(data.Result);
        }

        public Object Register(User user)
        {
            if (Exists(user.EmailId))
            {
                return new  { msg=$"{user.EmailId} already present" };
            }
            user.Id=new Guid();
            user.Password = GeneratePasswordHash(user.Password);
            _context.users.Add(user);
            _context.SaveChanges();
            return new {msg=$"User Added",created_user=ConstructReturnObject(user) };
        }

        private string GeneratePasswordHash(string password)
        {
            var sha = SHA256.Create();
            byte[] input=Encoding.UTF8.GetBytes(password);
            var hash= sha.ComputeHash(input);
            return Convert.ToBase64String(hash);
        }

        private bool Exists(string userEmail)
        {
            return (_context.users.Any(x => x.EmailId == userEmail));
        }
        private string GenerateToken(User data)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login","true"),
                new Claim("fullname",data.FullName),
                new Claim("email",data.EmailId),
                new Claim("id",data.Id.ToString()),
            };

            var token = new JwtSecurityToken(
                config["jwt:issuer"],
                config["jwt:audiance"],
                claims,
                //expires:DateTime.Now.AddMinutes(3),
                signingCredentials: credential
            );
            return (new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Object DeleteUser(Guid id)
        {
            var user = _context.users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new
                {
                   msg=$"user already deleted/No such user for {id}"
                };
            }
            _context.Entry(user).State = EntityState.Detached;
            _context.users.Remove(user);
            _context.SaveChanges();
            return new
            {
                msg=$"User deleted",
                user_mail=user.EmailId
            };
        }

        public Object UpdateUser(Guid id, User user)
        {
            var oldUser = _context.users.FirstOrDefault(x=>x.Id==id);
            if (oldUser == null)
            {
                return null;
            }
            _context.Entry(oldUser).State = EntityState.Detached;
            var updateData=_context.users.Update(user);
            List<string> fieldList = commonMethods.ModifiedDataList(oldUser, user, _context);
            _context.SaveChanges();

            return new
            {
                user_mail = user.EmailId,
                msg = $"User updated {id}",
                field_updated = fieldList,
                before_update = ConstructReturnObject(user),
                updated_on = DateTime.UtcNow.ToString("d/MM/yyyy hh:mm tt 'UTC'")
            };
        }

        public Object ResetPassword(Object credentials, string token)
        {
            string cred = credentials.ToString().Replace("\r","").Replace("\n", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace(" ", "");
            var user = _context.users.FirstOrDefault(x => x.Id == new Guid(commonMethods.getDataFromJwtToken(token)));
            if (user == null)
            {
                return new
                {
                    msg = $"User not found",
                };
            }
            string newPassword = ((cred.Split(',', StringSplitOptions.RemoveEmptyEntries))[1].Split(':', StringSplitOptions.RemoveEmptyEntries))[1];
            _context.Entry(user).State = EntityState.Detached;
            user.Password=GeneratePasswordHash(newPassword);
            _context.users.Update(user);
            _context.SaveChanges();
            return new
            {
                msg = $"User password updated for {user.EmailId}",
                updated_on = DateTime.UtcNow.ToString("d/MM/yyyy hh:mm tt 'UTC'")
            };
        }

        private Object ConstructReturnObject(User user)
        {
            return new
            {
                fullname = user.FullName,
                email_id = user.EmailId,
                salary = user.Salary,
                doj = user.DOJ,
                banking_return = user.Banking_return,
                stock_return=user.Stock_return,
                sip_return = user.SIP_return,
                total_savings = user.Total_savings,
            };
        }
    }
}
