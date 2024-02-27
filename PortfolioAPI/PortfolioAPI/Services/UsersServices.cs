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
        public UsersServices(APIDB context,IConfiguration config)
        {
            _context=context;
            this.config=config;
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

        public List<Object> Register(User user)
        {
            if (Exists(user.EmailId))
            {
                return new List<object> { $"{user.EmailId} already present" };
            }
            user.Id=new Guid();
            user.Password = GeneratePasswordHash(user.Password);
            _context.users.Add(user);
            _context.SaveChanges();
            return new List<object> {$"User Added",user };
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

        public object DeleteUser(Guid id)
        {
            var user = _context.users.FirstOrDefault(x => x.Id == id);
            string msg, user_mail;
            if (user == null)
            {
                
                return new object[]
                {
                   msg=$"user already deleted/No such user for {id}"
                };
            }
            _context.Entry(user).State = EntityState.Detached;
            _context.users.Remove(user);
            _context.SaveChanges();
            return new object[]
            {
                user_mail=user.EmailId,
                msg=$"User deleted"
            };
        }

        public object UpdateUser(Guid id, User user)
        {
            var oldUser = _context.users.FirstOrDefault(x=>x.Id==id);
            string msg, user_mail;
            if (oldUser == null)
            {
                return null;
            }
            _context.Entry(oldUser).State = EntityState.Detached;
            var updateData=_context.users.Update(user);
            _context.SaveChanges();
            return new object[]
            {
                user_mail=user.EmailId,
                msg=$"User updated {id}",
                updateData
            };
        }

        public object ResetPassword(Guid id, User user, string token)
        {
            throw new NotImplementedException();
        }
    }
}
