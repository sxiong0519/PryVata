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
    public class IncidentRepository : BaseRepository, IIncidentRepository
    {
        public IncidentRepository(IConfiguration configuration) : base(configuration) { }

        public List<Incident> GetAllIncidents()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id AS 'Incident Id', AssignedUserId, Title, i.Description AS IncidentDescription, DateReported,
                                        DateOccurred, i.FacilityId AS 'Facility Id', Confirmed, Reportable, DBRAId,

                                        FullName,

                                        FacilityName                                   

                                        FROM Incident i
                                        LEFT JOIN Facility f ON i.FacilityId = f.Id
                                        LEFT JOIN [User] u ON i.AssignedUserId = u.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Incident> incidents = new List<Incident>();

                    while (reader.Read())
                    {
                        incidents.Add(new Incident
                        {
                            Id = DbUtils.GetInt(reader, "Incident Id"),
                            AssignedUserId = DbUtils.GetInt(reader, "AssignedUserId"),
                            User = new User
                            {
                                FullName = DbUtils.GetString(reader, "FullName")
                            },
                            Title = DbUtils.GetString(reader, "Title"),
                            Description = DbUtils.GetString(reader, "IncidentDescription"),
                            DateReported = DbUtils.GetDateTime(reader, "DateReported"),
                            DateOccurred = DbUtils.GetDateTime(reader, "DateOccurred"),
                            FacilityId = DbUtils.GetInt(reader, "Facility Id"),
                            Confirmed = DbUtils.GetNullableBool(reader, "Confirmed"),
                            Reportable = DbUtils.GetNullableBool(reader, "Reportable"),
                            DBRAId = DbUtils.GetNullableInt(reader, "DBRAId")
                        });
                    }
                    reader.Close();
                    return incidents;
                }
            }
        }

        public List<Incident> GetAllIncidentsByUser(int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id AS 'Incident Id', AssignedUserId, Title, i.Description AS IncidentDescription, DateReported,
                                        DateOccurred, i.FacilityId AS 'Facility Id', Confirmed, Reportable, DBRAId,

                                        FullName,

                                        FacilityName

                                        FROM Incident i
                                        LEFT JOIN Facility f ON i.FacilityId = f.Id
                                        LEFT JOIN [User] u ON i.AssignedUserId = u.Id
                                        WHERE AssignedUserId = @userId";

                    cmd.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Incident> incidents = new List<Incident>();

                    while (reader.Read())
                    {
                        incidents.Add(new Incident
                        {
                            Id = DbUtils.GetInt(reader, "Incident Id"),
                            AssignedUserId = DbUtils.GetInt(reader, "AssignedUserId"),
                            User = new User
                            {
                                FullName = DbUtils.GetString(reader, "FullName")
                            },
                            Title = DbUtils.GetString(reader, "Title"),
                            Description = DbUtils.GetString(reader, "IncidentDescription"),
                            DateReported = DbUtils.GetDateTime(reader, "DateReported"),
                            DateOccurred = DbUtils.GetDateTime(reader, "DateOccurred"),
                            FacilityId = DbUtils.GetInt(reader, "Facility Id"),
                            Confirmed = DbUtils.GetNullableBool(reader, "Confirmed"),
                            Reportable = DbUtils.GetNullableBool(reader, "Reportable"),
                            DBRAId = DbUtils.GetNullableInt(reader, "DBRAId")
                        });
                    }
                    reader.Close();
                    return incidents;
                }
            }
        }

        public Incident GetIncidentById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id AS 'Incident Id', AssignedUserId, Title, i.Description AS IncidentDescription, DateReported,
                                        DateOccurred, i.FacilityId AS 'Facility Id', Confirmed, Reportable, DBRAId,

                                        FullName,
                                        
                                        n.Id AS NotesId, n.Description as Notes, n.ImageUrl AS Image,
                                
                                        FacilityName,
                    
                                        p.Id AS 'Patient Id', PatientNumber, FirstName, LastName

                                        FROM Incident i 
                                        LEFT JOIN Notes n ON i.Id = n.IncidentId
                                        LEFT JOIN Facility f ON i.FacilityId = f.Id
                                        LEFT JOIN [User] u ON i.AssignedUserId = u.Id
                                        LEFT JOIN PatientIncident pi ON i.Id = pi.IncidentId
                                        LEFT JOIN Patient p ON p.id = pi.PatientId
                                        WHERE i.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Incident incident = null;

                    while (reader.Read())
                    {
                        if (incident == null)
                            incident = new Incident
                            {
                                Id = DbUtils.GetInt(reader, "Incident Id"),
                                AssignedUserId = DbUtils.GetInt(reader, "AssignedUserId"),
                                User = new User
                                {
                                    FullName = DbUtils.GetString(reader, "FullName")
                                },
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "IncidentDescription"),
                                DateReported = DbUtils.GetDateTime(reader, "DateReported"),
                                DateOccurred = DbUtils.GetDateTime(reader, "DateOccurred"),
                                NotesId = DbUtils.GetNullableInt(reader, "NotesId"),
                                Notes = new List<Notes>(),
                                FacilityId = DbUtils.GetInt(reader, "Facility Id"),
                                Facility = new Facility
                                {
                                    FacilityName = DbUtils.GetString(reader, "FacilityName"),
                                },
                                Patient = new List<Patient>(),
                                Confirmed = DbUtils.GetNullableBool(reader, "Confirmed"),
                                Reportable = DbUtils.GetNullableBool(reader, "Reportable"),
                                DBRAId = DbUtils.GetNullableInt(reader, "DBRAId")
                            };

                        if (DbUtils.IsNotDbNull(reader, "NotesId"))
                        {
                            incident.Notes.Add(new Notes
                            {
                                Description = DbUtils.GetString(reader, "Notes"),
                                ImageUrl = DbUtils.GetNullableString(reader, "Image")
                            });
                        }

                        if (DbUtils.IsNotDbNull(reader, "Patient Id"))
                        {
                            if (!incident.Patient.Any(p => p.Id == DbUtils.GetInt(reader, "Patient Id")))
                            {
                                incident.Patient.Add(new Patient
                                {
                                    Id = DbUtils.GetInt(reader, "Patient Id"),
                                    PatientNumber = DbUtils.GetInt(reader, "PatientNumber"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName")
                                }) ; 
                            }
                        }

                    }
                    reader.Close();
                    return incident;
                }
            }
        }

        public void AddIncident(Incident incident)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Incident (AssignedUserId, Title, Description, DateReported, DateOccurred, FacilityId, Confirmed, Reportable, DBRAId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@user, @title, @description, @reported, @occurred, @facility, @confirmed, @reportable, @dbraId)";

                    cmd.Parameters.AddWithValue("@user", DbUtils.ValueOrDBNull(incident.AssignedUserId));
                    cmd.Parameters.AddWithValue("@title", DbUtils.ValueOrDBNull(incident.Title));
                    cmd.Parameters.AddWithValue("@description", DbUtils.ValueOrDBNull(incident.Description));
                    cmd.Parameters.AddWithValue("@reported", DbUtils.ValueOrDBNull(incident.DateReported));
                    cmd.Parameters.AddWithValue("@occurred", DbUtils.ValueOrDBNull(incident.DateOccurred));
                    cmd.Parameters.AddWithValue("@facility", DbUtils.ValueOrDBNull(incident.FacilityId));
                    cmd.Parameters.AddWithValue("@confirmed", DbUtils.ValueOrDBNull(incident.Confirmed));
                    cmd.Parameters.AddWithValue("@reportable", DbUtils.ValueOrDBNull(incident.Reportable));
                    cmd.Parameters.AddWithValue("@DBRAId", DbUtils.ValueOrDBNull(incident.DBRAId));

                    incident.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateIncident(Incident incident)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Incident 
                                        SET AssignedUserId = @user, 
                                        Title = @title, 
                                        Description = @description, 
                                        DateReported = @reported, 
                                        DateOccurred = @occurred, 
                                        FacilityId = @facility, 
                                        Confirmed = @confirmed, 
                                        Reportable = @reportable, 
                                        DBRAId = @dbraId
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@user", DbUtils.ValueOrDBNull(incident.AssignedUserId));
                    cmd.Parameters.AddWithValue("@title", DbUtils.ValueOrDBNull(incident.Title));
                    cmd.Parameters.AddWithValue("@description", DbUtils.ValueOrDBNull(incident.Description));
                    cmd.Parameters.AddWithValue("@reported", DbUtils.ValueOrDBNull(incident.DateReported));
                    cmd.Parameters.AddWithValue("@occurred", DbUtils.ValueOrDBNull(incident.DateOccurred));
                    cmd.Parameters.AddWithValue("@facility", DbUtils.ValueOrDBNull(incident.FacilityId));
                    cmd.Parameters.AddWithValue("@confirmed", DbUtils.ValueOrDBNull(incident.Confirmed));
                    cmd.Parameters.AddWithValue("@reportable", DbUtils.ValueOrDBNull(incident.Reportable));
                    cmd.Parameters.AddWithValue("@DBRAId", DbUtils.ValueOrDBNull(incident.DBRAId));
                    cmd.Parameters.AddWithValue("@id", DbUtils.ValueOrDBNull(incident.Id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteIncident(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Incident 
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
