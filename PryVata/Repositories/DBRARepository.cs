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
    public class DBRARepository : BaseRepository
    {
        public DBRARepository(IConfiguration configuration) : base(configuration) { }

        public List<DBRA> GetAllDBRAs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        d.Id AS DBRAId, IncidentTypeId, UserCompleteId, MethodId, RecipientId, CircumstanceId,
                                        DispositionId, InformationId, ControlsId,

                                        it.*, u.*, m.*, r.*, c.*, di.*, in.*, co.*
                                        FROM DBRA d
                                        LEFT JOIN IncidentType it ON d.IncidentTypeId = it.Id
                                        LEFT JOIN User u ON d.UserCompleteId = u.Id
                                        LEFT JOIN MethodType m ON d.MethodId = m.Id
                                        LEFT JOIN RecipientType r ON d.RecipientId = r.Id
                                        LEFT JOIN Circumstance c ON d.CircumstanceId = c.Id
                                        LEFT JOIN Disposition di ON d.DispositionId = di.Id
                                        LEFT JOIN Information in ON d.InformationId = in.Id
                                        LEFT JOIN Controls co ON d.ControlsId = co.Id";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<DBRA> DBRAs = new List<DBRA>();

                    while(reader.Read())
                    {
                        DBRAs.Add(new DBRA
                        {
                            Id = DbUtils.GetInt(reader, "DBRAId"),
                            IncidentTypeId = DbUtils.GetInt(reader, "IncidentTypeId"),
                            IncidentType = new IncidentType
                            {
                                Id = DbUtils.GetInt(reader, "IncidentTypeId"),
                                Type = DbUtils.GetString(reader, "Type"),
                                IncidentValue = DbUtils.GetInt(reader, "IncidentValue")
                            },
                            UserCompletedId = DbUtils.GetInt(reader, "UserCompletedId"),
                            User = new User
                            {
                                Id = DbUtils.GetInt(reader, "UserCompletedId"),
                                FullName = DbUtils.GetString(reader, "FullName")
                            },
                            MethodId = DbUtils.GetInt(reader, "MethodId"),
                            Method = new Method
                            {
                                Id = DbUtils.GetInt(reader, "MethodId"),
                                MethodType = DbUtils.GetString(reader, "Method"),
                                MethodValue = DbUtils.GetInt(reader, "MethodValue")
                            },
                            RecipientId = DbUtils.GetInt(reader, "RecipientId"),
                            Recipient = new Recipient
                            {
                                Id = DbUtils.GetInt(reader, "RecipientId"),
                                RecipientType = DbUtils.GetString(reader, "Recipient"),
                                RecipientValue = DbUtils.GetInt(reader, "Recipient Value")
                            },
                            CircumstanceId = DbUtils.GetInt(reader, "CircumstanceId"),
                            Circumstance = new Circumstance
                            {
                                Id = DbUtils.GetInt(reader, "CircumstanceId"),
                                Circumstances = DbUtils.GetString(reader, "Circumstance"),
                                CircumstanceValue = DbUtils.GetInt(reader, "CircumstanceValue")
                            },
                            DispositionId = DbUtils.GetInt(reader, "DispositionId"),
                            Disposition = new Dispositions
                            {
                                Id = DbUtils.GetInt(reader, "DispositionId"),
                                Disposition = DbUtils.GetString(reader, "Disposition"),
                                DispositionValue = DbUtils.GetInt(reader, "DispositionValue")
                            },
                            InformationId = DbUtils.GetInt(reader, "InformationId"),
                            Information = new Information
                            {
                                Id = DbUtils.GetInt(reader, "InformationId"),
                                InformationType = DbUtils.GetString(reader, "InformationType"),
                                InformationValue = DbUtils.GetInt(reader, "InformationValue")
                            },
                            ControlsId = DbUtils.GetInt(reader, "ControlsId"),
                            Control = new Controls
                            {
                                Id = DbUtils.GetInt(reader, "ControlsId"),
                                Control = DbUtils.GetString(reader, "Control"),
                                ControlValue = DbUtils.GetInt(reader, "ControlValue")
                            }
                        });
                    }
                    reader.Close();
                    return DBRAs;
                }
            }
        }
    }
}
