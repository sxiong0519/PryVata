using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{ 
    public class Incident
    {
        public int Id { get; set; }
        public int CreatorUserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateReported { get; set; }
        public DateTime DateOccurred { get; set; }
        public int NotesId { get; set; }
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
        public int PatientId { get; set; }
        public bool Confirmed { get; set; }
        public int DBRAId { get; set; }
        public bool Reportable { get; set; }
    }
}
