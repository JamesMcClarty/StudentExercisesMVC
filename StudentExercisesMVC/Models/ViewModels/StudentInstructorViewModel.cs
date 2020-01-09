using System.Collections.Generic;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class StudentInstructorViewModel
    {
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
    }
}