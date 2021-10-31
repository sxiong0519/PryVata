using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        void AddPatientIncident(int patientId, int incidentId);
        void DeletePatient(int id);
        List<Patient> GetAllPatients();
        Patient GetPatientById(int id);
    }
}