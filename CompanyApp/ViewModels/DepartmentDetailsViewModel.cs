using CompanyApp.DTOs;

namespace CompanyApp.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public DepartmentDTO Department { get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }

        public DepartmentDetailsViewModel()
        {

        }
    }
}
