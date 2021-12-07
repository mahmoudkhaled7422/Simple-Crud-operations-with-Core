using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Models
{
    public class DepartmentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Department")]
        public int deptId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int crsId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
    }
}
