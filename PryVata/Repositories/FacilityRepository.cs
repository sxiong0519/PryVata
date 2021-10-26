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
    public class FacilityRepository : BaseRepository, IFacilityRepository
    {
        public FacilityRepository(IConfiguration configuration) : base(configuration) { }

        public List<Facility> GetAllFacilities()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Facility";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Facility> facilities = new List<Facility>();

                    while (reader.Read())
                    {
                        facilities.Add(new Facility
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FacilityName = DbUtils.GetString(reader, "FacilityName"),
                            Address = DbUtils.GetString(reader, "Address"),
                            City = DbUtils.GetString(reader, "City"),
                            State = DbUtils.GetString(reader, "State"),
                            ZipCode = DbUtils.GetInt(reader, "ZipCode"),
                            isDeleted = DbUtils.GetNullableBool(reader, "isDeleted")
                        });
                    }

                    reader.Close();
                    return facilities;
                }
            }
        }

        public Facility GetFacilityById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Facility
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    Facility facility = null;

                    while (reader.Read())
                    {
                        if (facility == null)
                        {
                            facility = new Facility
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FacilityName = DbUtils.GetString(reader, "FacilityName"),
                                Address = DbUtils.GetString(reader, "Address"),
                                City = DbUtils.GetString(reader, "City"),
                                State = DbUtils.GetString(reader, "State"),
                                ZipCode = DbUtils.GetInt(reader, "ZipCode")
                            };
                        }
                    }
                    reader.Close();
                    return facility;
                }
            }
        }

        public void AddFacility(Facility facility)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Facility (FacilityName, Address, City, State, ZipCode, isDeleted)
                                        OUTPUT INSERTED.Id
                                        VALUES (@facilityName, @address, @city, @state, @zipCode, @isDeleted)";
                    cmd.Parameters.AddWithValue("@facilityName", facility.FacilityName);
                    cmd.Parameters.AddWithValue("@address", facility.Address);
                    cmd.Parameters.AddWithValue("@city", facility.City);
                    cmd.Parameters.AddWithValue("@state", facility.State);
                    cmd.Parameters.AddWithValue("@zipCode", facility.ZipCode);
                    cmd.Parameters.AddWithValue("@isDeleted", facility.isDeleted);

                    facility.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateFacility(Facility facility)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Facility 
                                        SET FacilityName = @facilityName, 
                                        Address = @address, 
                                        City = @city, 
                                        State = @state, 
                                        ZipCode = @zipCode, 
                                        isDeleted = @isDeleted
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@facilityName", facility.FacilityName);
                    cmd.Parameters.AddWithValue("@address", facility.Address);
                    cmd.Parameters.AddWithValue("@city", facility.City);
                    cmd.Parameters.AddWithValue("@state", facility.State);
                    cmd.Parameters.AddWithValue("@zipCode", facility.ZipCode);
                    cmd.Parameters.AddWithValue("@isDeleted", facility.isDeleted);
                    cmd.Parameters.AddWithValue("@id", facility.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFacility(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Facility WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
