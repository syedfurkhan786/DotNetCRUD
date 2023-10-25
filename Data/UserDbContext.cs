using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data{
    public class UserDbContext : DbContext{
        public UserDbContext(DbContextOptions<UserDbContext>options):base(options){
            
        }

        public virtual DbSet<EmployeeModel> EmployeeModel {get; set;}

        public virtual DbSet<UserModel> UserModel {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder){
           
            modelBuilder.Entity<UserModel>()
            .ToTable("tbl_user");

            modelBuilder.Entity<EmployeeModel>()
            .ToTable("tbl_employee");
        }
    }
}