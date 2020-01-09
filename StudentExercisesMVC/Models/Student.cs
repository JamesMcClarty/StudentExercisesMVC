using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(12, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(12, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        [Display(Name = "Slack Handle")]
        public string SlackHandle { get; set; }
        [Required]
        [Display(Name = "Cohort Number")]
        public int CohortId { get; set; }
        public Cohort Cohort { get; set; }
        public List<Exercise> Exercises{ get; set;}
    }
}
