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
    public class RecipientRepository : BaseRepository, IRecipientRepository
    {
        public RecipientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Recipient> GetAllRecipients()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM RecipientType";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Recipient> recipients = new List<Recipient>();

                    while (reader.Read())
                    {
                        recipients.Add(new Recipient
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            RecipientType = DbUtils.GetString(reader, "Recipient"),
                            RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                        });
                    }

                    reader.Close();
                    return recipients;
                }
            }
        }

        public Recipient GetRecipientById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM RecipientType
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Recipient recipient = null;

                    while (reader.Read())
                    {
                        if (recipient == null)
                        {
                            recipient = new Recipient
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                RecipientType = DbUtils.GetString(reader, "Recipient"),
                                RecipientValue = DbUtils.GetInt(reader, "RecipientValue")
                            };
                        }
                    }
                    reader.Close();
                    return recipient;
                }
            }
        }

        public void AddRecipient(Recipient recipient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RecipientType (Recipient, RecipientValue)
                                        OUTPUT INSERTED.Id
                                        VALUES (@recipientType, @recipientValue)";
                    cmd.Parameters.AddWithValue("@recipientType", recipient.RecipientType);
                    cmd.Parameters.AddWithValue("@recipientValue", recipient.RecipientValue);

                    recipient.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateRecipient(Recipient recipient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE RecipientType
                                        SET Recipient = @recipientType, 
                                        RecipientValue = @recipientValue
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@recipientType", recipient.RecipientType);
                    cmd.Parameters.AddWithValue("@recipientValue", recipient.RecipientValue);
                    cmd.Parameters.AddWithValue("@id", recipient.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRecipient(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RecipientType WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
