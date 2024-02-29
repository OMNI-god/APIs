using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PortfolioAPI.IServices;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersServices services;
        public UsersController(IUsersServices services)
        {
            this.services = services;
        }
        [HttpPost]
        public IActionResult UserRegistration(User user)
        {
            var data=services.Register(user);
            if(data.ToString().ToLower().Contains("already present"))
            {
                return Conflict(data);
            }
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Authenticate(string[] param)
        {
            if (param.Length < 2)
            {
                return BadRequest("Both Email and Password id required".ToJson());
            }
            string data= services.Authenticate(param);
            if(!string.IsNullOrEmpty(data))
            {
                return Ok(data.ToJson());
            }
            return NotFound();
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateUserDetails(User user)
        {
            return null;
        }
        [Authorize]
        [HttpPut]
        public IActionResult UserPasswordReset(Object credentials)
        {
            string token=getToken();
            var data = services.ResetPassword(credentials, token);
            return Ok(data);
        }
        private string getToken()
        {
            return Request.Headers["Authorization"];
        }
    }
}
