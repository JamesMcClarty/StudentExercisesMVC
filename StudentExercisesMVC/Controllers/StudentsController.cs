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

        // Post: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, StudentCreateViewModel model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE students
                                            SET first_name = @first,
                                                last_name = @last,
                                                slack_handle = @slack,
                                                cohort_id = @cohort
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
    }
}