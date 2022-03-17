using System.ComponentModel.DataAnnotations;

namespace CompanyApp.Models
{
    public class Department
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int BudgetYearly { get; set; }

        public Department()
        {

        }
    }
}
