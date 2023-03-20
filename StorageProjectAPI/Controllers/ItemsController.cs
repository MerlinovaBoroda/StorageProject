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

        public ItemsController(ItemsService itemsService) => _itemsService = itemsService;


        [HttpGet("/get/{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null) { return BadRequest(); }

            return Ok(existingItem);
        }


        [HttpGet("/get-all")]
        public async Task<IActionResult> Get()
        {
            var allItems = await _itemsService.GetAsync();
            if (allItems.Any())
            {
                return Ok(allItems);
            }
            return NotFound();
        }


        [HttpPost("/new-item")]
        public async Task<IActionResult> Create(ItemsModel item)
        {
            await _itemsService.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        

        [HttpPut("/update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ItemsModel item)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null)
                return BadRequest();

            item.Id = existingItem.Id;

            await _itemsService.UpdateAsync(item);

            return NoContent();
        }


        [HttpDelete("/delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null) { return BadRequest(); }

            await _itemsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
