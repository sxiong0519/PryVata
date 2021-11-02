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
    public class CircumstanceController : ControllerBase
    {
        private readonly ICircumstanceRepository _circumstanceRepository;

        public CircumstanceController(ICircumstanceRepository circumstanceRepository)
        {
            _circumstanceRepository = circumstanceRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var circumstance = _circumstanceRepository.GetAllCircumstances();

            return Ok(circumstance);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var circumstance = _circumstanceRepository.GetCircumstanceById(id);
            if (circumstance == null)
            {
                return NotFound();
            }
            return Ok(circumstance);
        }

        [HttpPost]
        public IActionResult Post(Circumstance circumstance)
        {
            _circumstanceRepository.AddCircumstance(circumstance);
            return CreatedAtAction("Get", new { id = circumstance.Id }, circumstance);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Circumstance circumstance)
        {
            if (id != circumstance.Id)
            {
                return BadRequest();
            }

            _circumstanceRepository.UpdateCircumstance(circumstance);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _circumstanceRepository.DeleteCircumstance(id);
            return NoContent();
        }
    }
}
