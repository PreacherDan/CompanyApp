//#nullable disable
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using CompanyApp.DTOs;
//using CompanyApp.Data;

//namespace CompanyApp.Controllers.API
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeDTOesController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public EmployeeDTOesController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/EmployeeDTOes
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployeeDTO()
//        {
//            //return await _context.EmployeeDTO.ToListAsync();
//        }

//        // GET: api/EmployeeDTOes/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<EmployeeDTO>> GetEmployeeDTO(int id)
//        {
//            //var employeeDTO = await _context.EmployeeDTO.FindAsync(id);

//            if (employeeDTO == null)
//            {
//                return NotFound();
//            }

//            return employeeDTO;
//        }

//        // PUT: api/EmployeeDTOes/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutEmployeeDTO(int id, EmployeeDTO employeeDTO)
//        {
//            if (id != employeeDTO.ID)
//            {
//                return BadRequest();
//            }

//            _context.Entry(employeeDTO).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!EmployeeDTOExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/EmployeeDTOes
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<EmployeeDTO>> PostEmployeeDTO(EmployeeDTO employeeDTO)
//        {
//            _context.EmployeeDTO.Add(employeeDTO);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetEmployeeDTO", new { id = employeeDTO.ID }, employeeDTO);
//        }

//        // DELETE: api/EmployeeDTOes/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteEmployeeDTO(int id)
//        {
//            var employeeDTO = await _context.EmployeeDTO.FindAsync(id);
//            if (employeeDTO == null)
//            {
//                return NotFound();
//            }

//            _context.EmployeeDTO.Remove(employeeDTO);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool EmployeeDTOExists(int id)
//        {
//            return _context.EmployeeDTO.Any(e => e.ID == id);
//        }
//    }
//}
