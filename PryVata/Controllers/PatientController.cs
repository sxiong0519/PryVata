using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PryVata.Models;
using PryVata.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PryVata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController (IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Patient> patients = _patientRepository.GetAllPatients();

            return Ok(patients);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var patient = _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public IActionResult Post(Patient patient)
        {
            _patientRepository.AddPatient(patient);
            _patientRepository.AddPatientIncident(patient.Id, patient.IncidentId);

            return CreatedAtAction("Get", new { id = patient.Id }, patient);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _patientRepository.DeletePatient(id);
            return NoContent();
        }

    }
}
