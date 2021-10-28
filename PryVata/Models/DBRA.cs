using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Models
{
    public class DBRA
    {
        public int Id { get; set; }
        public int UserCompletedId { get; set; }
        public User User { get; set; }
        public int ExceptionId { get; set; }
        public Exceptions Exception { get; set; }
        public int? MethodId { get; set; }
        public Method Method { get; set; }
        public int? RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public int? CircumstanceId { get; set; }
        public Circumstance Circumstance { get; set; }
        public int? DispositionId { get; set; }
        public Dispositions Disposition { get; set; }
        public int? InformationId { get; set; }
        public List<Information> Information { get; set; }
        public int? ControlsId { get; set; }
        public List<Controls> Control { get; set; }
        public int? IncidentId { get; set; }
    }
}
