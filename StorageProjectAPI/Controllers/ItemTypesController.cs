using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Models;
using StorageProject.Api.Services;

namespace StorageProject.Api.Controllers
{
    [Route("api/item-types/")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly ItemTypesService _itemTypeService;

        public ItemTypesController(ItemTypesService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> Get() 
        {
            var allItems = await _itemTypeService.GetAsync();
            if (allItems.Any())
            {
                return Ok(allItems);
            }
            return NotFound();
        }

        [HttpGet("get/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var existingType = await _itemTypeService.GetAsync(name);
            if (existingType is null) { return BadRequest("Item type not found"); }

            return Ok(existingType);
        }

        [HttpPost("new-item-type")]
        public async Task<IActionResult> Create(ItemTypeModel itemType)
        {
            var existingProvider = await _itemTypeService.GetAsync(itemType.Name);
            if (existingProvider is not null)
            {
                return BadRequest("Item Type already exists");
            }
            await _itemTypeService.CreateAsync(itemType);
            return CreatedAtAction(nameof(Get), new { code = itemType.Name }, itemType);
        }
    }
}
