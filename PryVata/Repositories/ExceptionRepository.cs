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
    public class ExceptionRepository : BaseRepository, IExceptionRepository
    {
        public ExceptionRepository(IConfiguration configuration) : base(configuration) { }

        public List<Exceptions> GetAllExceptions()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Exception";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exceptions> exceptions = new List<Exceptions>();

                    while (reader.Read())
                    {
                        exceptions.Add(new Exceptions
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Exception = DbUtils.GetString(reader, "Exception")
                        });
                    }

                    reader.Close();
                    return exceptions;
                }
            }
        }

        public Exceptions GetExceptionById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Exception
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Exceptions exception = null;

                    while (reader.Read())
                    {
                        if (exception == null)
                        {
                            exception = new Exceptions
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Exception = DbUtils.GetString(reader, "Exception")
                            };
                        }
                    }
                    reader.Close();
                    return exception;
                }
            }
        }

        public void AddException(Exceptions exception)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Exception (Exception)
                                        OUTPUT INSERTED.Id
                                        VALUES (@exception)";
                    cmd.Parameters.AddWithValue("@disposition", exception.Exception);

                    exception.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateException(Exceptions exception)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Exception
                                        SET Exception = @exception
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@exception", exception.Exception);
                    cmd.Parameters.AddWithValue("@id", exception.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteException(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Exception WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
