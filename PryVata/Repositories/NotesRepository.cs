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
    public class NotesRepository : BaseRepository, INotesRepository
    {
        public NotesRepository(IConfiguration configuration) : base(configuration) { }

        public List<Notes> GetAllNotes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Notes";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Notes> notes = new List<Notes>();

                    while (reader.Read())
                    {
                        notes.Add(new Notes
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Description = DbUtils.GetString(reader, "Description"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            IncidentId = DbUtils.GetInt(reader, "IncidentId")
                        });
                    }

                    reader.Close();
                    return notes;
                }
            }
        }

        public Notes GetNoteById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Notes
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Notes note = null;

                    while (reader.Read())
                    {
                        if (note == null)
                        {
                            note = new Notes
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                IncidentId = DbUtils.GetInt(reader, "IncidentId")
                            };
                        }
                    }
                    reader.Close();
                    return note;
                }
            }
        }

        public void AddNote(Notes note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Notes (Description, ImageUrl)
                                        OUTPUT INSERTED.Id
                                        VALUES (@description, @imageUrl)";
                    cmd.Parameters.AddWithValue("@description", note.Description);
                    cmd.Parameters.AddWithValue("@imageUrl", note.ImageUrl);

                    note.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateNote(Notes note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Notes
                                        SET Description = @description, 
                                        ImageUrl = @imageUrl
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@description", note.Description);
                    cmd.Parameters.AddWithValue("@imageUrl", DbUtils.ValueOrDBNull(note.ImageUrl));
                    cmd.Parameters.AddWithValue("@id", note.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNote(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Notes WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
