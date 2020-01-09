using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace StudentExercisesMVC.Models
{
    public class Cohort
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }

    }
}
