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
    public class RecipientController : ControllerBase
    {
        private readonly IRecipientRepository _recipientRepository;

        public RecipientController(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var recipient = _recipientRepository.GetAllRecipients();

            return Ok(recipient);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipient = _recipientRepository.GetRecipientById(id);
            if (recipient == null)
            {
                return NotFound();
            }
            return Ok(recipient);
        }

        [HttpPost]
        public IActionResult Post(Recipient recipient)
        {
            _recipientRepository.AddRecipient(recipient);
            return CreatedAtAction("Get", new { id = recipient.Id }, recipient);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Recipient recipient)
        {
            if (id != recipient.Id)
            {
                return BadRequest();
            }

            _recipientRepository.UpdateRecipient(recipient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipientRepository.DeleteRecipient(id);
            return NoContent();
        }
    }
}
