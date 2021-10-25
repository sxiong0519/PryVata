using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IPatientRepository
    {
        List<Patient> GetPatientsByIncident(int id);
    }
}