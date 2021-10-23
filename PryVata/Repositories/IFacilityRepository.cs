using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IFacilityRepository
    {
        void AddFacility(Facility facility);
        void DeleteFacility(int id);
        List<Facility> GetAllFacilities();
        Facility GetFacilityById(int id);
        void UpdateFacility(Facility facility);
    }
}