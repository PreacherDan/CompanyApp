using CompanyApp.DTOs;

namespace CompanyApp.ViewModels
{
    public class EmployeeFormViewModel
    {
        public EmployeeDTO Employee { get; set; }
        public List<DepartmentDTO> Departments { get; set; }

        public string FormType1 { get { return (this.Employee == null || this.Employee.ID == 0) ? "New" : "Edit"; } }

        public EmployeeFormViewModel()
        {

        }
    }
}
