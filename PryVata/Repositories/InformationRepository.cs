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
    public class InformationRepository : BaseRepository, IInformationRepository
    {
        public InformationRepository(IConfiguration configuration) : base(configuration) { }

        public List<Information> GetAllInformation()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Information";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Information> information = new List<Information>();

                    while (reader.Read())
                    {
                        information.Add(new Information
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            InformationType = DbUtils.GetString(reader, "InformationType"),
                            InformationValue = DbUtils.GetInt(reader, "InformationValue")
                        });
                    }

                    reader.Close();
                    return information;
                }
            }
        }

        public Information GetInformationById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Information
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Information information = null;

                    while (reader.Read())
                    {
                        if (information == null)
                        {
                            information = new Information
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                InformationType = DbUtils.GetString(reader, "InformationType"),
                                InformationValue = DbUtils.GetInt(reader, "InformationValue")
                            };
                        }
                    }
                    reader.Close();
                    return information;
                }
            }
        }

        public void AddInformation(Information information)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Information (InformationType, InformationValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@informationType, @informationValue)";
                    cmd.Parameters.AddWithValue("@informationType", information.InformationType);
                    cmd.Parameters.AddWithValue("@informationValue", information.InformationValue);

                    information.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateInformation(Information information)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Information
                                        SET InformationType = @informationType, 
                                        InformationValue = @informationValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@informationType", information.InformationType);
                    cmd.Parameters.AddWithValue("@informationValue", information.InformationValue);
                    cmd.Parameters.AddWithValue("@id", information.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteInformation(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Information WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
