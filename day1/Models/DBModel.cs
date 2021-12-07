using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Models
{
    public class DBModel : DbContext
    {
        public DBModel(DbContextOptions<DBModel> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmentCourse>().HasKey(sc => new { sc.deptId, sc.crsId });
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.Id, sc.crsId });
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DepartmentCourse> DepartmentCourses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

    }
}
