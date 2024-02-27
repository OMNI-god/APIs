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
        public string Number { get; set; }
        public string Bank_Name { get; set; }
        public string Type { get; set; }
        public string ROI { get; set; }
        [DataType(DataType.Date)]
        public DateTime Investment_Start_Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime Maturity_Date { get; set; }
        public double Investment_Amount { get; set; }
        public double Maturity_Amount { get; set; }
        public string Time_Left_To_Mature { get; set; }
        public Guid? UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime lastUpdate { get; set; }
    }
}