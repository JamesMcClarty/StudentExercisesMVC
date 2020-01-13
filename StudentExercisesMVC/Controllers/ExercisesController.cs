using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;

namespace StudentExercisesMVC.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IConfiguration _config;

        public ExercisesController(IConfiguration config)
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

        // GET: Exercises
        public ActionResult Index()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT e.id,
                    e.[name],
                    e.language
                    FROM exercises e";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();
                    while (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Language = reader.GetString(reader.GetOrdinal("language"))
                        };

                        exercises.Add(exercise);
                    }

                    reader.Close();

                    return View(exercises);
                }
            }
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT e.id,
                    e.[name],
                    e.language
                    FROM exercises e
                    WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = new Exercise();
                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Students = GetAllStudents(id)
                        };
                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            Exercise exercise = new Exercise();
            return View(exercise);
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Exercise model)
        {
            try
            {
                {
                    using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO exercises ([name], language)
                            VALUES (@name, @language)";
                            cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                            cmd.Parameters.Add(new SqlParameter("@language", model.Language));
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

        // GET: Exercises/Edit/5
        public ActionResult Edit(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT e.id,
                    e.[name],
                    e.language
                    FROM exercises e
                    WHERE id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Exercise exercise = new Exercise();
                    if (reader.Read())
                    {
                        exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Language = reader.GetString(reader.GetOrdinal("language"))
                        };
                    }

                    reader.Close();

                    return View(exercise);
                }
            }
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Exercise model)
        {
            try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE exercises
                                            SET [name] = @name, language = @language
                                            WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@id", model.Id));
                        cmd.Parameters.Add(new SqlParameter("@language", model.Language));
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

        // GET: Exercises/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT e.id,
                    e.[name],
                    e.language
                    FROM exercises e
                    WHERE e.id = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Exercise exercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Language = reader.GetString(reader.GetOrdinal("language"))
                        };

                        reader.Close();

                        return View(exercise);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
        }

        // POST: Exercises/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Exercise exercise)
        {
            try
            {
                var StudentsAffected = ReassignStudents(exercise.Id);

                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM exercises WHERE id = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", exercise.Id));

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

        //Helper methods

        private List<Student> GetAllStudents(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT students.id, students.first_name, students.last_name, students.slack_handle FROM students "+
                                      "LEFT JOIN studentexercises as se ON se.student_id = students.id " +
                                      "WHERE se.exercise_id = @exerciseId";
                    cmd.Parameters.Add(new SqlParameter("@exerciseId", id));

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

        private int ReassignStudents(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM studentexercises WHERE exercise_id = @exerciseId";
                    cmd.Parameters.Add(new SqlParameter("@exerciseId", id));

                    return cmd.ExecuteNonQuery();

                }
            }
        }
    }
}