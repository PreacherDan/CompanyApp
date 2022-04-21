using CompanyApp.DTOs;

namespace CompanyApp.Data
{
    public interface IEmployeeRepository
    {
        public IEnumerable<EmployeeDTO> GetEmployees();
        public EmployeeDTO GetEmployee(int id);
        public void CreateEmployee(EmployeeDTO employee);
        public EmployeeDTO UpdateEmployee(int id, EmployeeDTO employee);
        public void DeleteEmployee (int id);
    }
}
