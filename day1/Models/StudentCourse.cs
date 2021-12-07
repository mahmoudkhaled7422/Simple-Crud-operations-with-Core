using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace day1.Models
{
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Student")]
        public int Id { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int crsId { get; set; }
        [DefaultValue(0)]
        public int Degree { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
