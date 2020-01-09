using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Exercise {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string Language { get; set; }
        public List<Student> Students { get; set; }
    }
}
