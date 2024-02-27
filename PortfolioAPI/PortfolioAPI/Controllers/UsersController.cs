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
            if(data.Count==1)
            {
                return Conflict(data.ToJson());
            }
            user.Password = "";
            return Ok(user);
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
    }
}
