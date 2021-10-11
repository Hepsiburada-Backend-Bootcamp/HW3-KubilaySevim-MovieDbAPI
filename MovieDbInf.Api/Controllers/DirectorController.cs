using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDbInf.Application.Dto.Director;
using MovieDbInf.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDbInf.API.Controllers
{
    [Route("api/directors")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        private readonly ILogger<DirectorController> _logger;

        public DirectorController(IDirectorService directorService, ILogger<DirectorController> logger)
        {
            _logger = logger;
            _directorService = directorService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DirectorDto directorDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("In Director Add Method throws excepion");

                return BadRequest();
            }
            _logger.LogInformation("In DirectorController Add Method");
            _logger.LogTrace("Log Trace - In Director Add Method");
            await _directorService.Add(directorDto);
            return Ok(new {status = true, errors = ""}); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("In Director Get Method");

            List<DirectorDto> result = await _directorService.GetAll();
            return Ok(new {status = true, data = result, errors = ""});
            //var result = await _directorService.GetAll(_ => true);
            //return Ok(new { status = true, data = result, errors = "" });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _directorService.Get(id);

            if (result.Result != null)
            {
                return Ok(new {status = true, data = result.Result, errors = ""});
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete method directorcontroller");
            
            try
            {
                await _directorService.Delete(id);
                _logger.LogInformation("Delete method director controller accomplished");

                return Ok(new {status = true, errors = ""});
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }
    }
}