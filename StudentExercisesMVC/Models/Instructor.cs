using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Instructor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string SlackHandle { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Specialty { get; set; }
        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
    }
}
