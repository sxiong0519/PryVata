﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PryVata.Models;
using PryVata.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Repositories
{
    public class ControlRepository : BaseRepository, IControlRepository
    {
        public ControlRepository(IConfiguration configuration) : base(configuration) { }

        public List<Controls> GetAllControls()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Control";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Controls> controls = new List<Controls>();

                    while (reader.Read())
                    {
                        controls.Add(new Controls
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Control = DbUtils.GetString(reader, "Control"),
                            ControlValue = DbUtils.GetInt(reader, "ControlValue")
                        });
                    }

                    reader.Close();
                    return controls;
                }
            }
        }

        public Controls GetControlById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Control
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Controls control = null;

                    while (reader.Read())
                    {
                        if (control == null)
                        {
                            control = new Controls
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Control = DbUtils.GetString(reader, "Control"),
                                ControlValue = DbUtils.GetInt(reader, "ControlValue")
                            };
                        }
                    }
                    reader.Close();
                    return control;
                }
            }
        }

        public void AddControl(Controls control)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Control (Control, ControlValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@control, @controlValue";
                    cmd.Parameters.AddWithValue("@control", control.Control);
                    cmd.Parameters.AddWithValue("@controlValue", control.ControlValue);

                    control.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateControl(Controls control)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Control
                                        SET Control = @control, 
                                        ControlValue = @controlValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@control", control.Control);
                    cmd.Parameters.AddWithValue("@controlValue", control.ControlValue);
                    cmd.Parameters.AddWithValue("@id", control.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteControl(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Control WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
