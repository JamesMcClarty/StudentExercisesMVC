using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class StudentEditViewModel
    {
        public List<SelectListItem> Exercises { get; set; }
        public List<int> NewlyAssignedIds { get; set; }
        public List<int> SelectedIds;
        public Student Student { get; set; }
    }
}
