using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prova_ADO.NET.Interfaces;
using Prova_ADO.NET.Models;

namespace Prova_ADO.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly ILogger<PersonaController> _logger;
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(ILogger<PersonaController> logger, IPersonaRepository personaRepository)
        {
            _logger = logger;
            _personaRepository = personaRepository;
        }

        [HttpGet("/persone")]
        public ActionResult<List<Persona>> GetPersone()
        {
            try
            {
                var persone = _personaRepository.GetPersone();
                return Ok(persone);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la lettura dei dati: {ex.Message}");
                return StatusCode(500, "Errore durante l'accesso al database.");
            }
        }

        [HttpGet("/persone/{id}")]
        public ActionResult<Persona> GetPersona(long id)
        {
            try
            {
                var persona = _personaRepository.GetPersonaById(id);
                if (persona != null)
                {
                    return Ok(persona);
                }
                else
                {
                    return NotFound($"Non ci sono persone con l'Id {id} nel database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la lettura dei dati: {ex.Message}");
                return StatusCode(500, "Errore durante l'accesso al database.");
            }
        }

        [HttpPost("/persone")]
        public IActionResult InserisciPersona([FromBody] Persona persona)
        {
            try
            {
                var success = _personaRepository.InserisciPersona(persona);
                if (success)
                {
                    return Ok("Inserimento avvenuto con successo!");
                }
                else
                {
                    return BadRequest("Nessuna riga è stata inserita nel database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante l'inserimento dei dati: {ex.Message}");
                return StatusCode(500, "Errore durante l'accesso al database.");
            }
        }

        [HttpPut("/persone/modifica/{id}")]
        public IActionResult ModificaPersona(long id, [FromBody] Persona persona)
        {
            try
            {
                var success = _personaRepository.ModificaPersona(id, persona);
                if (success)
                {
                    return Ok("Modifica avvenuta con successo!");
                }
                else
                {
                    return NotFound($"Non ci sono persone con l'Id {id} nel database");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la modifica dei dati: {ex.Message}");
                return StatusCode(500, "Errore durante l'accesso al database.");
            }
        }

        [HttpDelete("/persone/elimina/{id}")]
        public IActionResult EliminaPersona(long id)
        {
            try
            {
                var success = _personaRepository.EliminaPersona(id);
                if (success)
                {
                    return Ok("Cancellazione avvenuta con successo!");
                }
                else
                {
                    return NotFound($"Nessuna persona trovata con l'ID {id}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errore durante la cancellazione dei dati: {ex.Message}");
                return StatusCode(500, "Errore durante l'accesso al database.");
            }
        }
    }
}

