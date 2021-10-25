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
    public class MethodRepository : BaseRepository, IMethodRepository
    {
        public MethodRepository(IConfiguration configuration) : base(configuration) { }

        public List<Method> GetAllMethods()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Method";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Method> methods = new List<Method>();

                    while (reader.Read())
                    {
                        methods.Add(new Method
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            MethodType = DbUtils.GetString(reader, "MethodType"),
                            MethodValue = DbUtils.GetInt(reader, "MethodValue")
                        });
                    }

                    reader.Close();
                    return methods;
                }
            }
        }

        public Method GetMethodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Method
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Method method = null;

                    while (reader.Read())
                    {
                        if (method == null)
                        {
                            method = new Method
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                MethodType = DbUtils.GetString(reader, "MethodType"),
                                MethodValue = DbUtils.GetInt(reader, "MethodValue")
                            };
                        }
                    }
                    reader.Close();
                    return method;
                }
            }
        }

        public void AddMethod(Method method)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Method (MethodType, MethodValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@methodType, @methodValue)";
                    cmd.Parameters.AddWithValue("@methodType", method.MethodType);
                    cmd.Parameters.AddWithValue("@methodValue", method.MethodValue);

                    method.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateMethod(Method method)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Method
                                        SET MethodType = @methodType, 
                                        MethodValue = @methodValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@methodType", method.MethodType);
                    cmd.Parameters.AddWithValue("@methodValue", method.MethodValue);
                    cmd.Parameters.AddWithValue("@id", method.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMethod(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Method WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
