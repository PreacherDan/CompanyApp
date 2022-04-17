using CompanyApp.DTOs;

namespace CompanyApp.Data
{
    public interface IDepartmentRepository
    {
        public IEnumerable<DepartmentDTO> GetDepartments();
        public DepartmentDTO GetDepartment(int id);
        public DepartmentDTO CreateDepartment(DepartmentDTO department);
        public DepartmentDTO UpdateDepartment(int id, DepartmentDTO department);
        public void DeleteDepartment(int id);
    }
}
