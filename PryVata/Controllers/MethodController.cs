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
    public class MethodController : ControllerBase
    {
        private readonly IMethodRepository _methodRepository;

        public MethodController(IMethodRepository methodRepository)
        {
            _methodRepository = methodRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var method = _methodRepository.GetAllMethods();

            return Ok(method);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var method = _methodRepository.GetMethodById(id);
            if (method == null)
            {
                return NotFound();
            }
            return Ok(method);
        }

        [HttpPost]
        public IActionResult Post(Method method)
        {
            _methodRepository.AddMethod(method);
            return CreatedAtAction("Get", new { id = method.Id }, method);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Method method)
        {
            if (id != method.Id)
            {
                return BadRequest();
            }

            _methodRepository.UpdateMethod(method);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _methodRepository.DeleteMethod(id);
            return NoContent();
        }
    }
}
