using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PryVata.Models;
using PryVata.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Repositories
{
    public class CircumstanceRepository : BaseRepository, ICircumstanceRepository
    {
        public CircumstanceRepository(IConfiguration configuration) : base(configuration) { }

        public List<Circumstance> GetAllCircumstances()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Circumstance";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Circumstance> circumstances = new List<Circumstance>();

                    while (reader.Read())
                    {
                        circumstances.Add(new Circumstance
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Circumstances = DbUtils.GetString(reader, "Circumstance"),
                            CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                        });
                    }

                    reader.Close();
                    return circumstances;
                }
            }
        }

        public Circumstance GetCircumstanceById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Circumstance
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Circumstance circumstance = null;

                    while (reader.Read())
                    {
                        if (circumstance == null)
                        {
                            circumstance = new Circumstance
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Circumstances = DbUtils.GetString(reader, "Circumstance"),
                                CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                            };
                        }
                    }
                    reader.Close();
                    return circumstance;
                }
            }
        }

        public void AddCircumstance(Circumstance circumstance)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Circumstance (Circumstance, CircumstanceValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@circumstance, @circumstanceValue)";
                    cmd.Parameters.AddWithValue("@circumstance", circumstance.Circumstances);
                    cmd.Parameters.AddWithValue("@circumstanceValue", circumstance.CircumstanceValue);

                    circumstance.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateCircumstance(Circumstance circumstance)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Circumstance
                                        SET Circumstance = @circumstance, 
                                        CircumstanceValue = @circumstanceValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@circumstance", circumstance.Circumstances);
                    cmd.Parameters.AddWithValue("@circumstanceValue", circumstance.CircumstanceValue);
                    cmd.Parameters.AddWithValue("@id", circumstance.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCircumstance(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Circumstance WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
