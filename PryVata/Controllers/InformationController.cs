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
    public class InformationController : ControllerBase
    {
        private readonly IInformationRepository _informationRepository;

        public InformationController(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var information = _informationRepository.GetAllInformation();

            return Ok(information);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var information = _informationRepository.GetInformationById(id);
            if (information == null)
            {
                return NotFound();
            }
            return Ok(information);
        }

        [HttpPost]
        public IActionResult Post(Information information)
        {
            _informationRepository.AddInformation(information);
            return CreatedAtAction("Get", new { id = information.Id }, information);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Information information)
        {
            if (id != information.Id)
            {
                return BadRequest();
            }

            _informationRepository.UpdateInformation(information);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _informationRepository.DeleteInformation(id);
            return NoContent();
        }
    }
}
