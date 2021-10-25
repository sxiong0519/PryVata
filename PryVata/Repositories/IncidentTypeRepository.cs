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
    public class IncidentTypeRepository : BaseRepository, IIncidentTypeRepository
    {
        public IncidentTypeRepository(IConfiguration configuration) : base(configuration) { }

        public List<IncidentType> GetAllIncidentTypes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM IncidentType";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<IncidentType> incidentTypes = new List<IncidentType>();

                    while (reader.Read())
                    {
                        incidentTypes.Add(new IncidentType
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Type = DbUtils.GetString(reader, "Type"),
                            IncidentValue = DbUtils.GetInt(reader, "IncidentValue")
                        });
                    }
                    reader.Close();
                    return incidentTypes;
                }
            }
        }

        public IncidentType GetIncidentTypeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM IncidentType
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    IncidentType incidentType = null;

                    if (reader.Read())
                    {
                        incidentType = new IncidentType
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Type = DbUtils.GetString(reader, "Type"),
                            IncidentValue = DbUtils.GetInt(reader, "IncidentValue")
                        };
                    }
                    reader.Close();
                    return incidentType;
                }
            }
        }

        public void AddIncidentType(IncidentType incidentType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO IncidentType (Type, IncidentValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@incidentType, @incidentValue)";
                    cmd.Parameters.AddWithValue("@incidentType", incidentType.Type);
                    cmd.Parameters.AddWithValue("@incidentValue", incidentType.IncidentValue);

                    incidentType.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateIncidentType(IncidentType incidentType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE IncidentType
                                        SET Type = @incidentType, 
                                        IncidentValue = @incidentValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@incidentType", incidentType.Type);
                    cmd.Parameters.AddWithValue("@incidentValue", incidentType.IncidentValue);
                    cmd.Parameters.AddWithValue("@id", incidentType.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteIncidentType(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM IncidentType WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
