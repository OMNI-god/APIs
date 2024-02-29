using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PortfolioAPI.Database;
using PortfolioAPI.IServices;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [Route("RESTapi/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class InvestmentsController : ControllerBase
    {
        private IInvestmentsServices services;
        public InvestmentsController(IInvestmentsServices services)
        {
            this.services=services;
        }

        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investment>>> Getinvestments()
        {
            string token = getToken();
            var data=services.GetInvestments(token).ToList();
            if(data.Count == 0)
            {
                return NoContent();
            }
            return data;
        }

        // GET: api/Investments/{int}x
        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestment(Guid id)
        {
            var data=services.GetInvestment(id);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        // PUT: api/Investments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Object>> PutInvestment(Guid id, Investment investment)
        {
            var data=services.UpdataInvestment(id,investment);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }

        // POST: api/Investments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investment>> PostInvestment(Investment investment)
        {
            string token = getToken();

            var investmentData = services.CreateInvestment(investment,token);

            return CreatedAtAction("GetInvestment", new { id = investment.Id }, investmentData);
        }

        private string getToken()
        {
            return Request.Headers["Authorization"];
        }

        // DELETE: api/Investments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Investment>> DeleteInvestment(Guid id)
        {
            var data=services.DeleteInvestment(id);
            if(data == null)
            {
                return NotFound();
            }
            return data;
        }

    }
}
