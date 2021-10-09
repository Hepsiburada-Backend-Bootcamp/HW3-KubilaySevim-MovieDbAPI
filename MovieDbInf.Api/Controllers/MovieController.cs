using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDbInf.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieDbInf.Application.Dto.Movie;

namespace MovieDbInf.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpPost]
        public  async Task<IActionResult> Add([FromBody] MovieDto movieDto)
        {           
            _logger.LogInformation("In MovieController Add Method");
            
            if (!ModelState.IsValid)
            {
                _logger.LogError("In MovieController Add Method model not valid");
                return BadRequest();
            }

            await _movieService.Add(movieDto);
            return Ok(new { status = true, errors = "" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var result =  await _movieService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _movieService.Get(id);

            if (result.Result != null)
            {
                return Ok(new {status = true, data = result.Result, errors = ""});
            }

            return NotFound();      
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete method movie controller");
            
            try
            {
                await _movieService.Delete(id);
                _logger.LogInformation("Delete method movie controller accomplished");

                return Ok(new {status = true, errors = ""});
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }
    }
}
