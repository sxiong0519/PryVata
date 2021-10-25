using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class Recipient
    {
        public int Id { get; set; }
        public string RecipientType { get; set; }
        public int RecipientValue { get; set; }

    }
}
