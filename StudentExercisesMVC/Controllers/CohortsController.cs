using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Controllers
{
    public class CohortsController : Controller
    {
        private readonly IConfiguration _config;

        public CohortsController(IConfiguration config)
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

        // GET: Cohorts
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.id,
                    c.[name]
                    FROM cohorts c";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> cohorts = new List<Cohort>();
                    while (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };

                        cohorts.Add(cohort);
                    }

                    reader.Close();

                    return View(cohorts);
                }
            }
        }

        // GET: Cohorts/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.id,
                    c.[name]
                    FROM cohorts c
                    WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Cohort cohort = new Cohort();
                    if (reader.Read())
                    {
                        cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Students = GetAllStudents(id),
                            Instructors = GetAllInstructors(id)
                        };
                    }

                    reader.Close();

                    return View(cohort);
                }
            }
        }

        // GET: Cohorts/Create
        public ActionResult Create()
        {
            Cohort cohort = new Cohort();
            return View(cohort);
        }

        // POST: Cohorts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Cohort model)
        {
            try
            {
                {
                    using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO cohorts ([name])
                            VALUES (@name)";
                            cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                            await cmd.ExecuteNonQueryAsync();

                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Cohorts/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.id,
                    c.[name]
                    FROM cohorts c
                    WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Cohort cohort = new Cohort();
                    if (reader.Read())
                    {
                        cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };
                    }

                    reader.Close();

                    return View(cohort);
                }
            }
        }

        // POST: Cohorts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Cohort cohort)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE cohorts
                                            SET [name] = @name
                                            WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@name", cohort.Name));
                        cmd.Parameters.Add(new SqlParameter("@id", cohort.Id));
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

        // GET: Cohorts/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.id,
                    c.[name]
                    FROM cohorts c
                    WHERE c.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        };

                        reader.Close();

                        return View(cohort);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // POST: Cohorts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Cohort model)
        {
            try
            {
                var StudentsAffected = ReassignStudents(model.Id);
                var InstructorsAffected = ReassignInstructors(model.Id);

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM cohorts WHERE id = @id";
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

        //Helper Methods
        //---------------------------------------
        private List<Student> GetAllStudents(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, first_name, last_name, slack_handle FROM students WHERE cohort_id = @cohortid";
                    cmd.Parameters.Add(new SqlParameter("@cohortid", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Student> students = new List<Student>();
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle"))
                        });
                    }

                    reader.Close();

                    return students;
                }
            }
        }

        private List<Instructor> GetAllInstructors(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, first_name, last_name, slack_handle, specialty FROM instructors WHERE cohort_id = @cohortid";
                    cmd.Parameters.Add(new SqlParameter("@cohortid", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        instructors.Add(new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            Specialty = reader.GetString(reader.GetOrdinal("specialty"))
                        });
                    }

                    reader.Close();

                    return instructors;
                }
            }
        }

        private int ReassignStudents (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Students SET cohort_id = NULL WHERE cohort_id = @cohortid";
                    cmd.Parameters.Add(new SqlParameter("@cohortid", id));

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        private int ReassignInstructors (int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Instructors SET cohort_id = NULL WHERE cohort_id = @cohortid";
                    cmd.Parameters.Add(new SqlParameter("@cohortid", id));

                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}