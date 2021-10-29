using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PryVata.Models;
using PryVata.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PryVata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IUserRepository _userRepository;

        public IncidentController(IIncidentRepository incidentRepository, IUserRepository userRepository)
        {
            _incidentRepository = incidentRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Incident> incidents = _incidentRepository.GetAllIncidents();

            return Ok(incidents);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var incident = _incidentRepository.GetIncidentById(id);
            if (incident == null)
            {
                return NotFound();
            }
            return Ok(incident);
        }

        [HttpGet("myIncidents")]
        public IActionResult MyIndex()
        {
            var currentUserProfile = GetCurrentUserProfile();

            var myIncidents = _incidentRepository.GetAllIncidentsByUser(currentUserProfile.Id);

            return Ok(myIncidents);
        }

        [HttpPost]
        public IActionResult Post(Incident incident)
        {
            _incidentRepository.AddIncident(incident);
            return CreatedAtAction("Get", new { id = incident.Id }, incident);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, Incident incident)
        {
            if (id != incident.Id)
            {
                return BadRequest();
            }

            _incidentRepository.UpdateIncident(incident);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _incidentRepository.DeleteIncident(id);
            return NoContent();
        }

        private User GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (firebaseUserId != null)
            {
                return _userRepository.GetByFirebaseUserId(firebaseUserId);
            }
            else
            {
                return null;
            }
        }
    }
}
