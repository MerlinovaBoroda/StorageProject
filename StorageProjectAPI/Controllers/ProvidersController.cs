using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Models;
using StorageProject.Api.Services;

namespace StorageProject.Api.Controllers
{
    [Route("api/providers/")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly ProvidersService _providersService;

        public ProvidersController(ProvidersService providersService)
        {
            _providersService = providersService;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> Get()
        {
            var allItems = await _providersService.GetAsync();
            if (allItems.Any())
            {
                return Ok(allItems);
            }
            return NotFound();
        }

        [HttpGet("get/{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            var existingPvider = await _providersService.GetAsyncById(id);
            if (existingPvider is null) { return BadRequest("Provider not found"); }

            return Ok(existingPvider);
        }

        [HttpGet("get/{code:length(8)}")]
        public async Task<IActionResult> Get(int code)
        {
            var existingPvider = await _providersService.GetAsync(code);
            if (existingPvider is null) { return BadRequest("Provider not found"); }

            return Ok(existingPvider);
        }

        [HttpGet("get/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var existingPvider = await _providersService.GetAsync(name);
            if (existingPvider is null) { return BadRequest("Provider not found"); }

            return Ok(existingPvider);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProviderModel provider)
        {
            var existingProvider = await _providersService.GetAsync(provider.EdrpouCode);
            if(existingProvider is not null)
            {
                return BadRequest("Provider already exists");
            }
            await _providersService.CreateAsync(provider);
            return CreatedAtAction(nameof(Get), new { code = provider.EdrpouCode }, provider);
        }

        [HttpDelete("delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingItem = await _providersService.GetAsyncById(id);

            if (existingItem is null) { return BadRequest(); }

            await _providersService.RemoveAsync(id);

            return NoContent();
        }
    }
}
