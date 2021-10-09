using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDbInf.Application.Dto.Genre;
using MovieDbInf.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbInf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GenreDto genreDto)
        {
            _logger.LogInformation("in GenreController Add methods");

            await _genreService.Add(genreDto);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _genreService.GetAll();
            if (result != null)
            {
                _logger.LogInformation("in GenreController GetAll methods");
                return Ok(new { status = true, data = result, errors = "" });
            }
            else
            {
                _logger.LogWarning("In Genre getAll Method result returns null");
                return BadRequest();
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _genreService.Get( id);

            if (result.Result != null)
            {
                return Ok(new {status = true, data = result.Result, errors = ""});
            }

            return NotFound();       
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete method genre controller");
            
            try
            {
                await _genreService.Delete(id);
                _logger.LogInformation("Delete method genre controller accomplished");

                return Ok(new {status = true, errors = ""});
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }
    }
}
