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
    public class IncidentTypeController : ControllerBase
    {
        private readonly IIncidentTypeRepository _incidentTypeRepository;

        public IncidentTypeController(IIncidentTypeRepository incidentTypeRepository)
        {
            _incidentTypeRepository = incidentTypeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var incidentType = _incidentTypeRepository.GetAllIncidentTypes();

            return Ok(incidentType);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var incidentType = _incidentTypeRepository.GetIncidentTypeById(id);
            if (incidentType == null)
            {
                return NotFound();
            }
            return Ok(incidentType);
        }

        [HttpPost]
        public IActionResult Post(IncidentType incidentType)
        {
            _incidentTypeRepository.AddIncidentType(incidentType);
            return CreatedAtAction("Get", new { id = incidentType.Id }, incidentType);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, IncidentType incidentType)
        {
            if (id != incidentType.Id)
            {
                return BadRequest();
            }

            _incidentTypeRepository.UpdateIncidentType(incidentType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _incidentTypeRepository.DeleteIncidentType(id);
            return NoContent();
        }
    }
}
