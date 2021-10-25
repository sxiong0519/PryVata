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
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        public PatientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Patient> GetPatientsByIncident(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Patient 
                                        WHERE IncidentId = @id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Patient> patients = new List<Patient>();

                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            PatientNumber = DbUtils.GetInt(reader, "PatientNumber"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName")
                        });
                    }

                    reader.Close();
                    return patients;
                }
            }
        }
    }
}
