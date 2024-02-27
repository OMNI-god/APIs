using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public double? Salary { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DOJ { get; set; }
        public double? Banking_return { get; set; }
        public double? Stock_return { get; set; }
        public double? SIP_return { get; set; }
        public double? Miscellaneous_return { get; set; }
        public double? Total_savings { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Last_update_date { get; set; }
        public List<Investment> Investments { get; }=new List<Investment>();
    }
}
