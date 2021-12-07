using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int crsId { get; set; }
        [Required]
        public string crsName { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
        public virtual List<DepartmentCourse> DepartmentCourses { get; set; }
    }
}
