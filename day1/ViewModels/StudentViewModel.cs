using day1.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Services
{
    public class StudentViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        [Required]
        public IFormFile Img { get; set; }
        [ForeignKey("department")]
        public int deptId { get; set; }
        public Department department { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
    }
}
