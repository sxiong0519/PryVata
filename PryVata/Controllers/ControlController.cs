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
    public class ControlController : ControllerBase
    {
        private readonly IControlRepository _controlRepository;

        public ControlController(IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var control = _controlRepository.GetAllControls();

            return Ok(control);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var control = _controlRepository.GetControlById(id);
            if (control == null)
            {
                return NotFound();
            }
            return Ok(control);
        }

        [HttpPost]
        public IActionResult Post(Controls control)
        {
            _controlRepository.AddControl(control);
            return CreatedAtAction("Get", new { id = control.Id }, control);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Controls control)
        {
            if (id != control.Id)
            {
                return BadRequest();
            }

            _controlRepository.UpdateControl(control);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _controlRepository.DeleteControl(id);
            return NoContent();
        }
    }
}
