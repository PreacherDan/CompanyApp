using CompanyApp.DTOs;

namespace CompanyApp.ViewModels
{
    public class EmployeeFormViewModel
    {
        public EmployeeDTO Employee { get; set; }
        public List<DepartmentDTO> Departments { get; set; }

        public int FormType { get { return (this.Employee == null || this.Employee.ID == 0 || this.Employee.ID==null) ? EmployeeFormViewModel.NewForm : EmployeeFormViewModel.EditForm; } }
        public string FormType1 { get { return (this.Employee == null || this.Employee.ID == 0) ? "New" : "Edit"; } }

        public const int NewForm = 0;
        public const int EditForm = 1;

        public EmployeeFormViewModel()
        {

        }
    }
}
