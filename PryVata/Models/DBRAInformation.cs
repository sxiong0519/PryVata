using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class DBRAInformation
    {
        public int Id { get; set; }
        public int DBRAId {get; set;}
        public int InformationId { get; set; }
        public Information Information { get; set; }
    }
}
