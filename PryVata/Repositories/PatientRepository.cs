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

        public List<Patient> GetAllPatients()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Patient 
                                        ";

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


        public Patient GetPatientById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Patient 
                                        WHERE PatientId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Patient patient = null;

                    if (reader.Read())
                    {
                        patient = new Patient
                        {

                            Id = DbUtils.GetInt(reader, "Id"),
                            PatientNumber = DbUtils.GetInt(reader, "PatientNumber"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName")
                        };
                    }

                    reader.Close();
                    return patient;
                }
            }
        }

        public void AddPatient(Patient patient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Patient (PatientNumber, FirstName, LastName)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@pn, @fn, @ln)";
                    cmd.Parameters.AddWithValue("@pn", patient.PatientNumber);
                    cmd.Parameters.AddWithValue("@fn", patient.FirstName);
                    cmd.Parameters.AddWithValue("@ln", patient.LastName);

                    patient.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void AddPatientIncident(int patientId, int incidentId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PatientIncident (PatientId, IncidentId)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@pi, @i)";
                    cmd.Parameters.AddWithValue("@pi", patientId);
                    cmd.Parameters.AddWithValue("@i", incidentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePatient(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Patient
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
