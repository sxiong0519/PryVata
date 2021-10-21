using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string FirebaseUserId { get; set; }
        public int UserTypeId { get; set; }
        public int? FacilityId { get; set; }
        public bool isActive { get; set; }
    }
}
