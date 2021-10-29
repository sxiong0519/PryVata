using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IIncidentRepository
    {
        void AddIncident(Incident incident);
        void DeleteIncident(int id);
        List<Incident> GetAllIncidents();
        Incident GetIncidentById(int id);
        void UpdateIncident(Incident incident);
        List<Incident> GetAllIncidentsByUser(int userId);
    }
}