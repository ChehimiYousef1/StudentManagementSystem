using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StudentManagementSystem_Console.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // LocalDB connection
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Configure table properties
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
