using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyApp.Data;
using CompanyApp.Models;
using CompanyApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Data
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private HttpClient _httpClient;

        public EmployeeRepository(HttpClient httpClient)
        {
            //this._httpClient = new HttpClient();// { BaseAddress = @"https://localhost:7235"; }
            this._httpClient = httpClient;// { BaseAddress = @"https://localhost:7235"; }            
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public IEnumerable<EmployeeDTO>? GetEmployees()
        {
            var empsFromDb = _httpClient.GetFromJsonAsync<List<Employee>>(@"api/employees/");
            return empsFromDb.Result.Select(e => new EmployeeDTO(e));
        }

        public EmployeeDTO? GetEmployee(int id)
        {
            var empFromDb = _httpClient.GetFromJsonAsync<EmployeeDTO>($"api/employees/{id}");
            return empFromDb.Result;
        }

        public void CreateEmployee(EmployeeDTO employee)
        {
            var result = _httpClient.PostAsJsonAsync<EmployeeDTO>($"api/employees", employee);
            //return result.Result.Content;
        }

        public void DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }        

        public EmployeeDTO UpdateEmployee(int id, EmployeeDTO employee)
        {
            throw new NotImplementedException();
        }
    }
}
