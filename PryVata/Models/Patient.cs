using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
