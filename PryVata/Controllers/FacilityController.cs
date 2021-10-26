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
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityRepository _facilityRepository;

        public FacilityController(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Facility> facilities = _facilityRepository.GetAllFacilities();

            return Ok(facilities);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var facility = _facilityRepository.GetFacilityById(id);
            if (facility == null)
            {
                return NotFound();
            }
            return Ok(facility);
        }

        [HttpPost]
        public IActionResult Post(Facility facility)
        {
            _facilityRepository.AddFacility(facility);
            return CreatedAtAction("Get", new { id = facility.Id }, facility);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Facility facility)
        {
            if (id != facility.Id)
            {
                return BadRequest();
            }

            _facilityRepository.UpdateFacility(facility);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _facilityRepository.DeleteFacility(id);
            return NoContent();
        }

    }
}
