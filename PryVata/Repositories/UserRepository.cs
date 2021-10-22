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
                    cmd.CommandText = @"SELECT * FROM [User] 
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
                            FacilityId = DbUtils.GetInt(reader, "FacilityId")                            
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
                    cmd.CommandText = @"SELECT * FROM [User]";

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
                                FacilityId = DbUtils.GetNullableInt(reader, "FacilityId")
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
