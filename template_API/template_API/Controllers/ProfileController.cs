using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using template_API.Models;
using template_API.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace template_API.Controllers
{


    [ApiController]
    [Route("[controller]/Sample")]
    public class ProfileController : Controller
    {

        private readonly SampleService _sampleService;
        private readonly ILogger<SampleService> _logger;

        public ProfileController(SampleService sampleService, ILogger<SampleService> logger)
        {
            _sampleService = sampleService;
            _logger = logger;

        }


        [HttpPost]
        public IActionResult Post(Sample sample)
        {
            try{
                this._sampleService.Post(sample);
            
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the sample.");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getByIdAsync(int id)
        {
            try
            {
                var sample = await this._sampleService.GetById(id);
                if (sample == null)
                {
                    return NotFound($"Sample with ID {id} not found.");
                }
                return Ok(sample);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error occurred while saving the sample.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var samples = await _sampleService.GetAll();
                return Ok(samples);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving samples.");
                return StatusCode(500, "An error occurred while retrieving the samples."); // Retourne 500 Internal Server Error
            }
        }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] Sample sample)
        {
            
            if (sample == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                var updated = await _sampleService.Update(sample); 
                if (updated == null)
                {
                    return NotFound($"Sample with ID {sample.Id} not found.");
                }
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the sample.");
                return StatusCode(500, "An error occurred while updating the sample."); // Retourne 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _sampleService.Delete(id); // Implémentation dans le service
                if (!deleted)
                {
                    return NotFound($"Sample with ID {id} not found."); // Retourne un 404 Not Found
                }
                return NoContent(); // Retourne 204 No Content si la suppression a réussi
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the sample.");
                return StatusCode(500, "An error occurred while deleting the sample."); // Retourne 500 Internal Server Error
            }
        }


    }
}
