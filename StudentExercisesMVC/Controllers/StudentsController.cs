using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;

        public StudentsController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET: Students
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT s.id,
                    s.first_name,
                    s.last_name,
                    s.slack_handle,
                    s.cohort_id
                    FROM students s";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();
                    while (reader.Read())
                    {
                        int cohortId = 0;
                        if (!reader.IsDBNull(reader.GetOrdinal("cohort_id")))
                        {
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"));
                        }

                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            CohortId = cohortId
                        };

                        students.Add(student);
                    }

                    reader.Close();

                    return View(students);
                }
            }
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT s.id,
                    s.first_name,
                    s.last_name,
                    s.slack_handle,
                    s.cohort_id
                    FROM students s
                    WHERE s.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int cohortId = 0;
                        if (!reader.IsDBNull(reader.GetOrdinal("cohort_id")))
                        {
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"));
                        }

                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            CohortId = cohortId
                        };

                        reader.Close();

                        return View(student);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // GET: Students/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new StudentCreateViewModel();
            var cohorts = GetAllCohorts();
            var selectItems = cohorts
                .Select(cohort => new SelectListItem
                {
                    Text = cohort.Name,
                    Value = cohort.Id.ToString()
                })
                .ToList();

            selectItems.Insert(0, new SelectListItem
            {
                Text = "Choose cohort...",
                Value = "0"
            });
            viewModel.Cohorts = selectItems;
            return View(viewModel);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentCreateViewModel model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO students
                ( first_name, last_name, slack_handle, cohort_id )
                VALUES
                ( @first_name, @last_name, @slack_handle, @cohort_id )";
                        cmd.Parameters.Add(new SqlParameter("@first_name", model.Student.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@last_name", model.Student.LastName));
                        cmd.Parameters.Add(new SqlParameter("@slack_handle", model.Student.SlackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohort_id", model.Student.CohortId));
                        await cmd.ExecuteNonQueryAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            StudentEditViewModel viewModel = new StudentEditViewModel();
            viewModel.Student = GetStudent(id);
            viewModel.SelectedIds = GetCurrentExerciseIDs(id);
            var exercises = GetAllExercises();
            if(viewModel.SelectedIds.Count != 0)
            {

            }
            var selectItems = exercises
                .Select(exercise => new SelectListItem
                {
                    Text = exercise.Name + " Subject: " + exercise.Language,
                    Value = exercise.Id.ToString()
                })
                .Where(p => !viewModel.SelectedIds.Contains(int.Parse(p.Value)))
                .ToList();

            viewModel.Exercises = selectItems;
            
            return View(viewModel);
        }

        // Post: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, StudentEditViewModel model)
        {
            try
            {

                //Create New Exercises
                foreach(int i in model.NewlyAssignedIds)
                {
                    CreateNewExercises(id, i);
                }

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE students
                                            SET first_name = @first,
                                                last_name = @last,
                                                slack_handle = @slack
                                            WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@first", model.Student.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@last", model.Student.LastName));
                        cmd.Parameters.Add(new SqlParameter("@slack", model.Student.SlackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohort", model.Student.CohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", model.Student.Id));
                        await cmd.ExecuteNonQueryAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT s.id,
                    s.first_name,
                    s.last_name,
                    s.slack_handle,
                    s.cohort_id
                    FROM students s
                    WHERE s.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"))
                        };

                        reader.Close();

                        return View(student);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Student model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM students WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@Id", model.Id));

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM cohorts";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        cohorts.Add(new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return cohorts;
                }
            }
        }

        private List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name, language FROM exercises";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Language = reader.GetString(reader.GetOrdinal("language"))
                        });
                    }

                    reader.Close();

                    return exercises;
                }
            }
        }

        private Student GetStudent(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, first_name, last_name, slack_handle FROM students WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Student student = new Student();
                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle"))
                        };
                    }

                    reader.Close();

                    return student;
                }
            }
        }

        private List<int> GetCurrentExerciseIDs(int studentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT exercise_id from studentexercises WHERE studentexercises.student_id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id",studentId));

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<int> intList = new List<int>();
                    while (reader.Read())
                    {
                        intList.Add(reader.GetInt32(reader.GetOrdinal("exercise_id")));
                    }

                    reader.Close();

                    return intList;
                }
            }
        }

        private int CreateNewExercises (int studentID, int newExercise)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO studentexercises (student_id, exercise_id) 
                                        VALUES (@studentID, @newExercise)";
                    cmd.Parameters.Add(new SqlParameter("@studentID", studentID));
                    cmd.Parameters.Add(new SqlParameter("@newExercise", newExercise));

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}