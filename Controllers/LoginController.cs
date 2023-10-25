using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase{

        private readonly UserDbContext _context;

        public LoginController(UserDbContext dbContext){
            _context = dbContext;
        }
    
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var userDetails = _context.UserModel.AsQueryable();
            return Ok(userDetails);
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] UserModel userObj)
        {
            if(userObj == null){
               return BadRequest(); 
            }
            else{
                _context.UserModel.Add(userObj);
                _context.SaveChanges(); 
                return Ok(new {
                    StatusCode = 200,
                    Message = "Signed Up Successfully"
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel userObj)
        {
            if(userObj == null){
               return BadRequest(); 
            }
            else{
                var user = _context.UserModel.Where(a=>
                a.Email == userObj.Email && a.Password == userObj.Password).FirstOrDefault();
                    if(user != null){
                        return Ok(new
                         {
                        StatusCode = 200,
                        Message = "Logged In Successfully"
                    });
                    
                }
                else{
                    return NotFound(new {
                        StatusCode = 404,
                        Message = "User not found"
                    });
                }
            }
        }
    
    }
    
}

     