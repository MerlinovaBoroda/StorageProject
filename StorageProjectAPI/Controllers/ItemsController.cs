using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageProject.Api.Models;
using StorageProject.Api.Services;

namespace StorageProject.Api.Controllers
{
    [Route("api/items/")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _itemsService;
        private readonly ItemTypesService _itemTypesService;
        private readonly ProvidersService _providersService;

        public ItemsController(ItemsService itemsService, ItemTypesService itemTypesService, ProvidersService providersService)
        {
            _itemsService = itemsService;
            _itemTypesService = itemTypesService;
            _providersService = providersService;
        }


        [HttpGet("get/{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null) { return BadRequest(); }

            return Ok(existingItem);
        }


        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var allItems = await _itemsService.GetAsync();
            if (allItems.Any())
            {
                return Ok(allItems);
            }
            return NotFound();
        }


        [HttpPost("new-item")]
        public async Task<IActionResult> Create(ItemModel item)
        {
            var allItems = _itemsService.GetAsync();
            var itemType = _itemTypesService.GetAsync(item.ItemType.Name);
            var provider = _providersService.GetAsync(item.Provider.EdrpouCode);

            if (allItems.Result.Any(x => x.SerialNumber == item.SerialNumber) &&
                allItems.Result.Any(x => x.Name == item.Name))
            {
                return BadRequest();
            }

            if (itemType.Result is null)
            {
                await _itemTypesService.CreateAsync(item.ItemType);
            }
            if (provider.Result is null)
            {
                await _providersService.CreateAsync(item.Provider);
            }
            item.ItemType.Id = itemType.Result?.Id;
            item.Provider.Id = provider.Result?.Id;

            await _itemsService.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }
        

        [HttpPut("update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ItemModel item)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null)
                return BadRequest();

            item.Id = existingItem.Id;

            await _itemsService.UpdateAsync(item);

            return NoContent();
        }


        [HttpDelete("delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null) { return BadRequest(); }

            await _itemsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
