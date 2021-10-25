using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IIncidentTypeRepository
    {
        void AddIncidentType(IncidentType incidentType);
        void DeleteIncidentType(int id);
        List<IncidentType> GetAllIncidentTypes();
        IncidentType GetIncidentTypeById(int id);
        void UpdateIncidentType(IncidentType incidentType);
    }
}