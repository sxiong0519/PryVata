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
    public class ExceptionController : ControllerBase
    {
        private readonly IExceptionRepository _exceptionRepository;

        public ExceptionController(IExceptionRepository exceptionRepository)
        {
            _exceptionRepository = exceptionRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var exception = _exceptionRepository.GetAllExceptions();

            return Ok(exception);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var exception = _exceptionRepository.GetExceptionById(id);
            if (exception == null)
            {
                return NotFound();
            }
            return Ok(exception);
        }

        [HttpPost]
        public IActionResult Post(Exceptions exception)
        {
            _exceptionRepository.AddException(exception);
            return CreatedAtAction("Get", new { id = exception.Id }, exception);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Exceptions exception)
        {
            if (id != exception.Id)
            {
                return BadRequest();
            }

            _exceptionRepository.UpdateException(exception);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _exceptionRepository.DeleteException(id);
            return NoContent();
        }
    }
}
