using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class InstructorEditViewModel
    {
        public List<SelectListItem> Cohorts { get; set; }
        public Instructor Instructor { get; set; }

        public InstructorEditViewModel() { }

        public InstructorEditViewModel(SqlConnection Connection)
        {

            string sql = $@"
                            SELECT
                                c.Id,
                                c.[name]
                            FROM cohorts c";

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Cohort> CurrentCohorts = new List<Cohort>();

                    while (reader.Read())
                    {
                        Cohort cohort = new Cohort
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };

                        CurrentCohorts.Add(cohort);
                    }

                    var selectedItems = CurrentCohorts.Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                        .ToList();

                    Cohorts = selectedItems;

                    reader.Close();
                }
            }
        }
    }
}
