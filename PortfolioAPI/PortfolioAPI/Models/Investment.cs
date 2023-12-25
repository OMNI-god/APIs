using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAPI.Models
{
    public class Investment
    {
        [Key] 
        public Guid Id { get; set; }
    }
}