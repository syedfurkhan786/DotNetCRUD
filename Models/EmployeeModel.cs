using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models{
    public class EmployeeModel{
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public int Salary { get; set; }
        public string Email { get; set; }
    }
}