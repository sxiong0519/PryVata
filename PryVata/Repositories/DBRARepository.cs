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
    public class DBRARepository : BaseRepository, IDBRARepository
    {
        public DBRARepository(IConfiguration configuration) : base(configuration) { }

        public List<DBRA> GetAllDBRAs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        d.Id AS DBRAId, UserCompleteId, MethodId, RecipientId, CircumstanceId,
                                        DispositionId, IncidentId,

                                        u.*, m.*, r.*, c.*, di.*, i.*, co.*
                                        
                                        FROM DBRA d
                                        LEFT JOIN [User] u ON d.UserCompleteId = u.Id
                                        LEFT JOIN MethodType m ON d.MethodId = m.Id
                                        LEFT JOIN RecipientType r ON d.RecipientId = r.Id
                                        LEFT JOIN Circumstance c ON d.CircumstanceId = c.Id
                                        LEFT JOIN Disposition di ON d.DispositionId = di.Id
                                        LEFT JOIN DBRAInformation db ON d.Id = db.DBRAId
                                        LEFT JOIN Information i ON i.Id = db.InformationId
                                        LEFT JOIN DBRAControls dc ON d.Id = dc.DBRAId
                                        LEFT JOIN Controls co ON dc.ControlsId = co.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<DBRA> DBRAs = new List<DBRA>();

                    while (reader.Read())
                    {
                        DBRAs.Add(new DBRA
                        {
                            Id = DbUtils.GetInt(reader, "DBRAId"),
                            UserCompletedId = DbUtils.GetInt(reader, "UserCompleteId"),
                            User = new User
                            {
                                FullName = DbUtils.GetString(reader, "FullName")
                            },
                            MethodId = DbUtils.GetInt(reader, "MethodId"),
                            Method = new Method
                            {
                                MethodType = DbUtils.GetString(reader, "Method"),
                                MethodValue = DbUtils.GetInt(reader, "MethodValue")
                            },
                            RecipientId = DbUtils.GetInt(reader, "RecipientId"),
                            Recipient = new Recipient
                            {
                                RecipientType = DbUtils.GetString(reader, "Recipient"),
                                RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                            },
                            CircumstanceId = DbUtils.GetInt(reader, "CircumstanceId"),
                            Circumstance = new Circumstance
                            {
                                Circumstances = DbUtils.GetString(reader, "Circumstance"),
                                CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                            },
                            DispositionId = DbUtils.GetInt(reader, "DispositionId"),
                            Disposition = new Dispositions
                            {
                                Disposition = DbUtils.GetString(reader, "Disposition"),
                                DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                            },
                            IncidentId = DbUtils.GetInt(reader, "IncidentId")
                        });
                    }
                    reader.Close();
                    return DBRAs;
                }
            }
        }

        public DBRA GetDBRAById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        d.Id AS DBRAId, UserCompleteId, MethodId, RecipientId, CircumstanceId,
                                        DispositionId, IncidentId,

                                        FullName,                                        
                                        Method, MethodValue,
                                        Recipient, RecipientValue,
                                        Circumstance, CircumstanceValue,
                                        Disposition, DispositionValue,
                                        i.Id AS InformationId, InformationType, InformationValue,
                                        co.Id AS ControlsId, Controls, ControlsValue,
                                        db.Id AS DBRAInformationId

                                        FROM DBRA d
                                        LEFT JOIN [User] u ON d.UserCompleteId = u.Id
                                        LEFT JOIN MethodType m ON d.MethodId = m.Id
                                        LEFT JOIN RecipientType r ON d.RecipientId = r.Id
                                        LEFT JOIN Circumstance c ON d.CircumstanceId = c.Id
                                        LEFT JOIN Disposition di ON d.DispositionId = di.Id
                                        LEFT JOIN DBRAInformation db ON d.Id = db.DBRAId
                                        LEFT JOIN Information i ON i.Id = db.InformationId
                                        LEFT JOIN DBRAControls dc ON d.Id = dc.DBRAId
                                        LEFT JOIN Controls co ON dc.ControlsId = co.Id
                                        WHERE d.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    DBRA DBRA = null;

                    while (reader.Read())
                    {
                        if (DBRA == null)
                        {
                            DBRA = new DBRA
                            {
                                Id = DbUtils.GetInt(reader, "DBRAId"),
                                UserCompletedId = DbUtils.GetInt(reader, "UserCompleteId"),
                                User = new User
                                {
                                    FullName = DbUtils.GetString(reader, "FullName")
                                },
                                MethodId = DbUtils.GetInt(reader, "MethodId"),
                                Method = new Method
                                {
                                    MethodType = DbUtils.GetString(reader, "Method"),
                                    MethodValue = DbUtils.GetInt(reader, "MethodValue")
                                },
                                RecipientId = DbUtils.GetInt(reader, "RecipientId"),
                                Recipient = new Recipient
                                {
                                    RecipientType = DbUtils.GetString(reader, "Recipient"),
                                    RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                                },
                                CircumstanceId = DbUtils.GetInt(reader, "CircumstanceId"),
                                Circumstance = new Circumstance
                                {
                                    Circumstances = DbUtils.GetString(reader, "Circumstance"),
                                    CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                                },
                                DispositionId = DbUtils.GetInt(reader, "DispositionId"),
                                Disposition = new Dispositions
                                {
                                    Disposition = DbUtils.GetString(reader, "Disposition"),
                                    DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                                },
                                Information = new List<Information>(),
                                Control = new List<Controls>(),
                                IncidentId = DbUtils.GetInt(reader, "IncidentId")
                            };
                        }

                            if (DbUtils.IsNotDbNull(reader, "InformationId"))
                            {
                                if(!DBRA.Information.Any(i => i.Id == DbUtils.GetInt(reader, "InformationId")))
                                DBRA.Information.Add(new Information
                                {
                                        Id = DbUtils.GetInt(reader, "InformationId"),
                                        InformationType = DbUtils.GetString(reader, "InformationType"),
                                        InformationValue = DbUtils.GetInt(reader, "InformationValue")                                
                                });
                            }

                            if (DbUtils.IsNotDbNull(reader, "ControlsId"))
                            {
                                if(!DBRA.Control.Any(c => c.Id == DbUtils.GetInt(reader, "ControlsId")))
                                DBRA.Control.Add(new Controls
                                {
                                    Id = DbUtils.GetInt(reader, "ControlsId"),
                                    Control = DbUtils.GetString(reader, "Controls"),
                                    ControlValue = DbUtils.GetInt(reader, "ControlsValue")
                                });
                            }
                        }
                    reader.Close();
                    return DBRA;
                }
                    
                }
            }
        

        public void AddDBRA(DBRA DBRA, int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO DBRA (UserCompletedId, MethodId, RecipientId, CircumstanceId, DispositionId, IncidentId)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@user, @method, @recipient, @circumstance, @disposition, @incident)";

                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@method", DbUtils.ValueOrDBNull(DBRA.MethodId));
                    cmd.Parameters.AddWithValue("@recipient", DbUtils.ValueOrDBNull(DBRA.RecipientId));
                    cmd.Parameters.AddWithValue("@circumstance", DbUtils.ValueOrDBNull(DBRA.CircumstanceId));
                    cmd.Parameters.AddWithValue("@disposition", DbUtils.ValueOrDBNull(DBRA.DispositionId));
                    cmd.Parameters.AddWithValue("@incident", DbUtils.ValueOrDBNull(DBRA.IncidentId));

                    DBRA.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateDBRA(DBRA DBRA, int userId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE DBRA 
                                        SET UserCompletedId = @user, 
                                        MethodId = @method, 
                                        RecipientId = @recipient, 
                                        CircumstanceId = @circumstance, 
                                        DispositionId =  @disposition, 
                                        IncidentId = @incident
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@method", DBRA.MethodId);
                    cmd.Parameters.AddWithValue("@recipient", DBRA.RecipientId);
                    cmd.Parameters.AddWithValue("@circumstance", DBRA.CircumstanceId);
                    cmd.Parameters.AddWithValue("@disposition", DBRA.DispositionId);
                    cmd.Parameters.AddWithValue("@incident", DBRA.IncidentId);
                    cmd.Parameters.AddWithValue("@id", DBRA.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDBRA(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM DBRA
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

