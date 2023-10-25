using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase{

        private readonly UserDbContext _context;

        public EmployeeController(UserDbContext dbContext){
            _context = dbContext;
        }

        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employeeObj)
        {
            if(employeeObj == null){
               return BadRequest(); 
            }
            else{
                _context.EmployeeModel.Add(employeeObj);
                _context.SaveChanges(); 
                return Ok(new {
                    StatusCode = 200,
                    Message = "Employee Added Successfully"
                });
            }
        }

        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeModel employeeObj)
        {
            if(employeeObj == null){
               return BadRequest(); 
            }
            
            var user = _context.EmployeeModel.AsNoTracking().FirstOrDefault(x => x.Id == employeeObj.Id);
                
            if(user == null){
                return NotFound(new {
                    StatusCode = 404,
                    Message = "Employee not found"
                });
            }
            else{
                _context.Entry(employeeObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                         {
                        StatusCode = 200,
                        Message = "Employee Updated Successfully"
                });
            }  
        }

        [HttpDelete("delete_employee/{id}")]
        public IActionResult DeleteEmployee(int id){
            var user = _context.EmployeeModel.Find(id);

            if(user == null){
                return NotFound(new {
                    StatusCode = 404,
                    Message = "Employee not found"
                });
            }      
            else{
                _context.Remove(user);
                _context.SaveChanges();
                 return Ok(new
                         {
                        StatusCode = 200,
                        Message = "Employee Deleted Successfully"
                });
            }      
        }

        [HttpGet("get_all_employees")]
        public IActionResult GetEmployees()
        {
            var employeeDetails = _context.EmployeeModel.AsQueryable();
            return Ok(new
                         {
                        StatusCode = 200,
                        EmployeeDetails = employeeDetails
            });
        }
        
        [HttpGet("get_employee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.EmployeeModel.Find(id);
            if(employee == null){
                return NotFound(new {
                    StatusCode = 404,
                    Message = "Employee not found"
                });
            }  
            else{
                return Ok(new
                         {
                        StatusCode = 200,
                        EmployeeDetail  = employee
            });
            } 
        }
    
    }
    
}

     