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
    public class DispositionRepository : BaseRepository, IDispositionRepository
    {
        public DispositionRepository(IConfiguration configuration) : base(configuration) { }

        public List<Dispositions> GetAllDispositions()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Disposition";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Dispositions> dispositions = new List<Dispositions>();

                    while (reader.Read())
                    {
                        dispositions.Add(new Dispositions
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Disposition = DbUtils.GetString(reader, "Disposition"),
                            DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                        });
                    }

                    reader.Close();
                    return dispositions;
                }
            }
        }

        public Dispositions GetDispositionById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Disposition
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Dispositions disposition = null;

                    while (reader.Read())
                    {
                        if (disposition == null)
                        {
                            disposition = new Dispositions
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Disposition = DbUtils.GetString(reader, "Disposition"),
                                DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                            };
                        }
                    }
                    reader.Close();
                    return disposition;
                }
            }
        }

        public void AddDisposition(Dispositions disposition)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Disposition (Disposition, DispositionValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@disposition, @dispositionValue)";
                    cmd.Parameters.AddWithValue("@disposition", disposition.Disposition);
                    cmd.Parameters.AddWithValue("@dispositionValue", disposition.DispositionValue);

                    disposition.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateDisposition(Dispositions disposition)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Disposition
                                        SET Disposition = @disposition, 
                                        DispositionValue = @dispositionValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@disposition", disposition.Disposition);
                    cmd.Parameters.AddWithValue("@dispositionValue", disposition.DispositionValue);
                    cmd.Parameters.AddWithValue("@id", disposition.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDisposition(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Disposition WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
