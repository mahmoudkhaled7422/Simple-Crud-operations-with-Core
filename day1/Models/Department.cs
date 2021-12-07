using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deptId { get; set; }
        [Required]
        public string deptName { get; set; }
        public List<Student> students { get; set; }
        public virtual List<DepartmentCourse> DepartmentCourses { get; set; }
    }
}
