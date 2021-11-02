using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class DBRAController : ControllerBase
    {
        private readonly IDBRARepository _dbraRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInformationRepository _informationRepository;
        private readonly IControlRepository _controlRepository;

        public DBRAController(IDBRARepository dbraRepository, IUserRepository userRepository, IInformationRepository informationRepository, 
                IControlRepository controlRepository)
        {
            _dbraRepository = dbraRepository;
            _userRepository = userRepository;
            _informationRepository = informationRepository;
            _controlRepository = controlRepository;
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
            var allDbra = _dbraRepository.GetAllDBRAs();
            var total = 0;

            if (allDbra.Any(d => d.IncidentId == DBRA.IncidentId))
            {
                _dbraRepository.DeleteDBRAByIncident(DBRA.IncidentId);
            }

            _dbraRepository.AddDBRA(DBRA, currentUserProfile.Id);

            if (DBRA.ExceptionId == 5)
            {
                
                foreach (int infoId in DBRA.InformationIds)
                {
                    _dbraRepository.AddDBRAInformation(infoId, DBRA.Id);
                    var infoRisk = _informationRepository.GetInformationById(infoId);
                    total += infoRisk.InformationValue;
                }

                foreach(int controlId in DBRA.ControlIds)
                {
                    _dbraRepository.AddDBRAControls(controlId, DBRA.Id);
                    var controlRisk = _controlRepository.GetControlById(controlId);
                    total += controlRisk.ControlValue;
                }

                var dbraRisk = _dbraRepository.GetDBRAById(DBRA.Id);

                total += (dbraRisk.Method.MethodValue + dbraRisk.Recipient.RecipientValue + dbraRisk.Circumstance.CircumstanceValue + dbraRisk.Disposition.DispositionValue);

            }

            _dbraRepository.UpdateRiskValue(DBRA.Id, total);

            return CreatedAtAction("Get", new { id = DBRA.Id }, DBRA);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, DBRA dbra)
        {
            var currentUserProfile = GetCurrentUserProfile();
            var total = 0;

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
                    var infoRisk = _informationRepository.GetInformationById(infoId);
                    total += infoRisk.InformationValue;
                }

                foreach (int controlId in dbra.ControlIds)
                {
                    _dbraRepository.AddDBRAControls(controlId, dbra.Id);
                    var controlRisk = _controlRepository.GetControlById(controlId);
                    total += controlRisk.ControlValue;
                }
                var dbraRisk = _dbraRepository.GetDBRAById(dbra.Id);
                
                total += (dbraRisk.Method.MethodValue + dbraRisk.Recipient.RecipientValue + dbraRisk.Circumstance.CircumstanceValue + dbraRisk.Disposition.DispositionValue);
                
            }

            _dbraRepository.UpdateRiskValue(dbra.Id, total);

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

        private void FindRiskValue(int id)
        {
            var dbra = _dbraRepository.GetDBRAById(id);
            var total = 0;
            if(dbra.ExceptionId == 5)
            {
                total += (dbra.Method.MethodValue + dbra.Recipient.RecipientValue);
            }


        }
    }
}
