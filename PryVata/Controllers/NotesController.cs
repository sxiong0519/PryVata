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
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notes = _notesRepository.GetAllNotes();

            return Ok(notes);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var notes = _notesRepository.GetNoteById(id);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Post(Notes notes)
        {
            _notesRepository.AddNote(notes);
            return CreatedAtAction("Get", new { id = notes.Id }, notes);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Notes notes)
        {
            if (id != notes.Id)
            {
                return BadRequest();
            }

            _notesRepository.UpdateNote(notes);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _notesRepository.DeleteNote(id);
            return NoContent();
        }
    }
}
