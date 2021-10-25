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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public User GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.*, ut.Name, FacilityName 
                                        FROM [User] u 
                                        LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                                        LEFT JOIN Facility f on u.FacilityId = f.Id
                                        WHERE FirebaseUserId = @firebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    User user = null;

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            FullName = DbUtils.GetString(reader, "FullName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                            UserType = new UserType
                            {
                                Name = DbUtils.GetString(reader, "Name")
                            },
                            FacilityId = DbUtils.GetNullableInt(reader, "FacilityId"),
                            Facility = new Facility
                            {
                                FacilityName = DbUtils.GetString(reader, "FacilityName")
                            }
                        };
                    }

                    reader.Close();
                    return user;
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.*, ut.Name, FacilityName 
                                        FROM [User] u 
                                        LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                                        LEFT JOIN Facility f on u.FacilityId = f.Id";

                    List<User> users = new List<User>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                FullName = DbUtils.GetString(reader, "FullName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                UserType = new UserType
                                {
                                    Name = DbUtils.GetString(reader, "Name")
                                },
                                FacilityId = DbUtils.GetNullableInt(reader, "FacilityId"),
                                Facility = new Facility
                                {
                                    FacilityName = DbUtils.GetString(reader, "FacilityName")
                                }
                            };
                            users.Add(user);
                        }
                    }

                    return users;
                }
            }
        }
    }
}
