using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PryVata.Models;
using PryVata.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PryVata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBRAController : ControllerBase
    {
        private readonly IDBRARepository _dbraRepository;
        private readonly IUserRepository _userRepository;

        public DBRAController(IDBRARepository dbraRepository, IUserRepository userRepository)
        {
            _dbraRepository = dbraRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<DBRA> dbras = _dbraRepository.GetAllDBRAs();

            return Ok(dbras);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dbra = _dbraRepository.GetDBRAById(id);
            if (dbra == null)
            {
                return NotFound();
            }
            return Ok(dbra);
        }

        [HttpPost]
        public IActionResult Post(DBRA DBRA)
        {
            
            var currentUserProfile = GetCurrentUserProfile();
            _dbraRepository.AddDBRA(DBRA, currentUserProfile.Id);
            if (DBRA.ExceptionId == 5)
            {
                foreach(int infoId in DBRA.InformationIds)
                {
                    _dbraRepository.AddDBRAInformation(infoId, DBRA.Id);
                }

                foreach(int controlId in DBRA.ControlIds)
                {
                    _dbraRepository.AddDBRAControls(controlId, DBRA.Id);
                }
            }
            return CreatedAtAction("Get", new { id = DBRA.Id }, DBRA);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, DBRA dbra)
        {
            var currentUserProfile = GetCurrentUserProfile();

            if (id != dbra.Id)
            {
                return BadRequest();
            }

            _dbraRepository.UpdateDBRA(dbra, currentUserProfile.Id);
            _dbraRepository.DeleteDBRAControls(dbra.Id);
            _dbraRepository.DeleteDBRAInformation(dbra.Id);
            if (dbra.ExceptionId == 5)
            {
                foreach (int infoId in dbra.InformationIds)
                {
                    _dbraRepository.AddDBRAInformation(infoId, dbra.Id);
                }

                foreach (int controlId in dbra.ControlIds)
                {
                    _dbraRepository.AddDBRAControls(controlId, dbra.Id);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dbraRepository.DeleteDBRA(id);
            return NoContent();
        }


        private User GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (firebaseUserId != null)
            {
                return _userRepository.GetByFirebaseUserId(firebaseUserId);
            }
            else
            {
                return null;
            }
        }
    }
}
