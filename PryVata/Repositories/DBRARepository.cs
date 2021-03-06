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
                                        d.Id AS DBRAId, ExceptionId, UserCompleteId, MethodId, RecipientId, CircumstanceId,
                                        DispositionId, IncidentId, RiskValue,

                                        u.*, m.*, r.*, c.*, di.*, i.*, co.*
                                        
                                        FROM DBRA d
                                        LEFT JOIN [User] u ON d.UserCompleteId = u.Id
                                        LEFT JOIN Exception e ON d.ExceptionId = e.Id
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
                        DBRA dbra = new DBRA
                        {
                            Id = DbUtils.GetInt(reader, "DBRAId"),
                            UserCompletedId = DbUtils.GetInt(reader, "UserCompleteId"),
                            ExceptionId = DbUtils.GetInt(reader, "ExceptionId"),
                            MethodId = DbUtils.GetNullableInt(reader, "MethodId"),
                            RecipientId = DbUtils.GetNullableInt(reader, "RecipientId"),
                            CircumstanceId = DbUtils.GetNullableInt(reader, "CircumstanceId"),
                            DispositionId = DbUtils.GetNullableInt(reader, "DispositionId"),
                            IncidentId = DbUtils.GetInt(reader, "IncidentId"),
                            RiskValue = DbUtils.GetNullableInt(reader, "RiskValue")
                        };
                    
                    if (DbUtils.IsNotDbNull(reader, "MethodId"))
                    {
                        dbra.Method = new Method
                        {
                            MethodType = DbUtils.GetString(reader, "Method"),
                            MethodValue = DbUtils.GetInt(reader, "MethodValue")
                        };
                    }
                    if (DbUtils.IsNotDbNull(reader, "RecipientId"))
                    {
                        dbra.Recipient = new Recipient
                        {
                            RecipientType = DbUtils.GetString(reader, "Recipient"),
                            RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                        };
                    }
                    if (DbUtils.IsNotDbNull(reader, "CircumstanceId"))
                    {
                        dbra.Circumstance = new Circumstance
                        {
                            Circumstances = DbUtils.GetString(reader, "Circumstance"),
                            CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                        };
                    }
                    if (DbUtils.IsNotDbNull(reader, "DispositionId"))
                    {
                        dbra.Disposition = new Dispositions
                        {
                            Disposition = DbUtils.GetString(reader, "Disposition"),
                            DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                        };
                    }
                        DBRAs.Add(dbra);
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
                                        d.Id AS DBRAId, ExceptionId, UserCompleteId, MethodId, RecipientId, CircumstanceId,
                                        DispositionId, IncidentId, RiskValue,

                                        FullName,
                                        Exception,
                                        Method, MethodValue,
                                        Recipient, RecipientValue,
                                        Circumstance, CircumstanceValue,
                                        Disposition, DispositionValue,
                                        i.Id AS InformationId, InformationType, InformationValue,
                                        co.Id AS ControlsId, Controls, ControlsValue,
                                        db.Id AS DBRAInformationId

                                        FROM DBRA d
                                        LEFT JOIN [User] u ON d.UserCompleteId = u.Id
                                        LEFT JOIN Exception e ON d.ExceptionId = e.Id
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
                                ExceptionId = DbUtils.GetInt(reader, "ExceptionId"),
                                Exception = new Exceptions 
                                { 
                                    Exception = DbUtils.GetString(reader, "Exception") 
                                },
                                MethodId = DbUtils.GetNullableInt(reader, "MethodId"),
                                RecipientId = DbUtils.GetNullableInt(reader, "RecipientId"),                                
                                CircumstanceId = DbUtils.GetNullableInt(reader, "CircumstanceId"),                               
                                DispositionId = DbUtils.GetNullableInt(reader, "DispositionId"),                                
                                Information = new List<Information>(),
                                Control = new List<Controls>(),
                                IncidentId = DbUtils.GetInt(reader, "IncidentId"),
                                RiskValue = DbUtils.GetNullableInt(reader, "RiskValue")
                            };
                        }
                            if (DbUtils.IsNotDbNull(reader,"MethodId"))
                        {
                            DBRA.Method = new Method
                            {
                                MethodType = DbUtils.GetString(reader, "Method"),
                                MethodValue = DbUtils.GetInt(reader, "MethodValue")
                            };
                        }
                        if (DbUtils.IsNotDbNull(reader, "RecipientId"))
                        {
                            DBRA.Recipient = new Recipient
                            {
                                RecipientType = DbUtils.GetString(reader, "Recipient"),
                                RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                            };
                        }
                        if (DbUtils.IsNotDbNull(reader, "CircumstanceId"))
                        {
                            DBRA.Circumstance = new Circumstance
                            {
                                Circumstances = DbUtils.GetString(reader, "Circumstance"),
                                CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                            };
                        }
                        if (DbUtils.IsNotDbNull(reader, "DispositionId"))
                        {
                            DBRA.Disposition = new Dispositions
                            {
                                Disposition = DbUtils.GetString(reader, "Disposition"),
                                DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
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
                    cmd.CommandText = @"INSERT INTO DBRA (UserCompleteId, ExceptionId, MethodId, RecipientId, CircumstanceId, DispositionId, IncidentId)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@user, @exception, @method, @recipient, @circumstance, @disposition, @incident)";

                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@exception", DbUtils.ValueOrDBNull(DBRA.ExceptionId));
                    cmd.Parameters.AddWithValue("@method", DbUtils.ValueOrDBNull(DBRA.MethodId));
                    cmd.Parameters.AddWithValue("@recipient", DbUtils.ValueOrDBNull(DBRA.RecipientId));
                    cmd.Parameters.AddWithValue("@circumstance", DbUtils.ValueOrDBNull(DBRA.CircumstanceId));
                    cmd.Parameters.AddWithValue("@disposition", DbUtils.ValueOrDBNull(DBRA.DispositionId));
                    cmd.Parameters.AddWithValue("@incident", DbUtils.ValueOrDBNull(DBRA.IncidentId));
                    

                    DBRA.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void AddDBRAInformation(int infoId, int DBRAId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO DBRAInformation (DBRAId, InformationId)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@dbraId, @informationId)";

                    cmd.Parameters.AddWithValue("@dbraId", DbUtils.ValueOrDBNull(DBRAId));
                    cmd.Parameters.AddWithValue("@informationId", DbUtils.ValueOrDBNull(infoId));

                   cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddDBRAControls(int controlId, int DBRAId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO DBRAControls (DBRAId, ControlsId)
                                        OUTPUT INSERTED.Id 
                                        VALUES (@dbraId, @controlsId)";

                    cmd.Parameters.AddWithValue("@dbraId", DbUtils.ValueOrDBNull(DBRAId));
                    cmd.Parameters.AddWithValue("@controlsId", DbUtils.ValueOrDBNull(controlId));

                    cmd.ExecuteNonQuery();
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
                                        SET UserCompleteId = @user, 
                                        ExceptionId = @exceptionId,
                                        MethodId = @method, 
                                        RecipientId = @recipient, 
                                        CircumstanceId = @circumstance, 
                                        DispositionId =  @disposition
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@exceptionId", DbUtils.ValueOrDBNull(DBRA.ExceptionId));
                    cmd.Parameters.AddWithValue("@method", DbUtils.ValueOrDBNull(DBRA.MethodId));
                    cmd.Parameters.AddWithValue("@recipient", DbUtils.ValueOrDBNull(DBRA.RecipientId));
                    cmd.Parameters.AddWithValue("@circumstance", DbUtils.ValueOrDBNull(DBRA.CircumstanceId));
                    cmd.Parameters.AddWithValue("@disposition", DbUtils.ValueOrDBNull(DBRA.DispositionId));
                    cmd.Parameters.AddWithValue("@id", DBRA.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDBRAInformation(int DBRAId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM DBRAInformation
                                        WHERE DBRAId = @id";

                    cmd.Parameters.AddWithValue("@id", DBRAId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDBRAControls(int DBRAId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM DBRAControls
                                        WHERE DBRAId = @id";

                    cmd.Parameters.AddWithValue("@id", DBRAId);

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

        public void DeleteDBRAByIncident(int? id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM DBRA
                                        WHERE IncidentId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRiskValue(int id, int total)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE DBRA
                                        SET RiskValue = @value
                                        WHERE Id = @dbraId";

                    cmd.Parameters.AddWithValue("@dbraId", id);
                    cmd.Parameters.AddWithValue("@value", total);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

