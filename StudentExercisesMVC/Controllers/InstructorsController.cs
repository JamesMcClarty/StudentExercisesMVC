using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Models.ViewModels;

namespace StudentExercisesMVC.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IConfiguration _config;

        public InstructorsController(IConfiguration config)
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

        // GET: Instructors
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT i.id,
                    i.first_name,
                    i.last_name,
                    i.slack_handle,
                    i.specialty,
                    i.cohort_id
                    FROM instructors i";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Instructor> instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        int cohortId = 0;
                        if (!reader.IsDBNull(reader.GetOrdinal("cohort_id")))
                        {
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"));
                        }
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            Specialty = reader.GetString(reader.GetOrdinal("specialty")),
                            CohortId = cohortId
                        };

                        instructors.Add(instructor);
                    }

                    reader.Close();

                    return View(instructors);
                }
            }
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT i.id,
                    i.first_name,
                    i.last_name,
                    i.slack_handle,
                    i.specialty,
                    i.cohort_id
                    FROM instructors i
                    WHERE i.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int cohortId = 0;
                        if (!reader.IsDBNull(reader.GetOrdinal("cohort_id")))
                        {
                            cohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"));
                        }

                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            Specialty = reader.GetString(reader.GetOrdinal("specialty")),
                            CohortId = cohortId
                        };

                        reader.Close();

                        return View(instructor);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            var viewModel = new InstructorCreateViewModel();
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

        // POST: Instructors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InstructorCreateViewModel model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO instructors
                                            ( first_name, last_name, slack_handle, specialty, cohort_id )
                                            VALUES
                                            ( @first_name, @last_name, @slack_handle, @specialty, @cohort_id )";
                        cmd.Parameters.Add(new SqlParameter("@first_name", model.instructor.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@last_name", model.instructor.LastName));
                        cmd.Parameters.Add(new SqlParameter("@slack_handle", model.instructor.SlackHandle));
                        cmd.Parameters.Add(new SqlParameter("@specialty", model.instructor.Specialty));
                        cmd.Parameters.Add(new SqlParameter("@cohort_id", model.instructor.CohortId));
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

        // GET: Instructors/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = new InstructorCreateViewModel();
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

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, InstructorCreateViewModel model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE instructors
                                            SET first_name = @first,
                                                last_name = @last,
                                                slack_handle = @slack,
                                                specialty = @specialty,
                                                cohort_id = @cohort
                                            WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@first", model.instructor.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@last", model.instructor.LastName));
                        cmd.Parameters.Add(new SqlParameter("@slack", model.instructor.SlackHandle));
                        cmd.Parameters.Add(new SqlParameter("@specialty", model.instructor.Specialty));
                        cmd.Parameters.Add(new SqlParameter("@cohort", model.instructor.CohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));
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

        // GET: Instructors/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT i.id,
                    i.first_name,
                    i.last_name,
                    i.slack_handle,
                    i.specialty,
                    i.cohort_id
                    FROM instructors i
                    WHERE i.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("slack_handle")),
                            Specialty = reader.GetString(reader.GetOrdinal("slack_handle")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("cohort_id"))
                        };

                        reader.Close();

                        return View(instructor);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // POST: Instructors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Instructor model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM instructors WHERE id = @id";
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