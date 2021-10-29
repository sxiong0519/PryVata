using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
