using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Data;
using CompanyApp.Models;
using CompanyApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CompanyApp.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private HttpClient _httpClient = null;

        public DepartmentRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public IEnumerable<DepartmentDTO> GetDepartments()
        {
            var depts = _httpClient.GetFromJsonAsync<IEnumerable<Department>>("api/departments");
            return depts.Result.Select(d => new DepartmentDTO(d));
        }

        public DepartmentDTO GetDepartment(int id)
        {
            var dept = _httpClient.GetFromJsonAsync<Department>($"api/departments/{id}");
            //DepartmentDTO deptDto = JsonSerializer.Deserialize<DepartmentDTO>(dept.Result);
            return new DepartmentDTO(dept.Result);
        }

        public DepartmentDTO CreateDepartment(DepartmentDTO department)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }
        public DepartmentDTO UpdateDepartment(int id, DepartmentDTO department)
        {
            throw new NotImplementedException();
        }
    }
}
