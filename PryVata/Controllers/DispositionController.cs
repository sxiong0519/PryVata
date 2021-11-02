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
    public class DispositionController : ControllerBase
    {
        private readonly IDispositionRepository _dispositionRepository;

        public DispositionController(IDispositionRepository dispositionRepository)
        {
            _dispositionRepository = dispositionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var disposition = _dispositionRepository.GetAllDispositions();

            return Ok(disposition);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var disposition = _dispositionRepository.GetDispositionById(id);
            if (disposition == null)
            {
                return NotFound();
            }
            return Ok(disposition);
        }

        [HttpPost]
        public IActionResult Post(Dispositions disposition)
        {
            _dispositionRepository.AddDisposition(disposition);
            return CreatedAtAction("Get", new { id = disposition.Id }, disposition);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Dispositions disposition)
        {
            if (id != disposition.Id)
            {
                return BadRequest();
            }

            _dispositionRepository.UpdateDisposition(disposition);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dispositionRepository.DeleteDisposition(id);
            return NoContent();
        }
    }
}
