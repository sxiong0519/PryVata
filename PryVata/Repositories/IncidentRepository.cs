using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PryVata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Repositories
{
    public class IncidentRepository : BaseRepository
    {
        public IncidentRepository(IConfiguration configuration) : base(configuration) { }

        /*public List<Incident> GetAllIncidents()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();

                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.*, "
                }
            }
        }*/
    }
}
