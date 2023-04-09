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

            var existingItemType = await _itemTypesService.GetAsyncById(existingItem.ItemTypeId);
            var existingProvider = await _providersService.GetAsyncById(existingItem.ProviderId);

            if (existingItemType is not null) { existingItem.ItemType = existingItemType; }
            if (existingProvider is not null) { existingItem.Provider = existingProvider; }

            return Ok(existingItem);
        }


        [HttpGet("get/all")]
        public async Task<IActionResult> Get()
        {
            var allItems = await _itemsService.GetAsync();
            if (allItems.Any())
            {
                foreach (var item in allItems)
                {
                    var existingItemType = await _itemTypesService.GetAsyncById(item.ItemTypeId);
                    var existingProvider = await _providersService.GetAsyncById(item.ProviderId);

                    if (existingItemType is not null) { item.ItemType = existingItemType; }
                    if (existingProvider is not null) { item.Provider = existingProvider; }
                }
                return Ok(allItems);
            }
            return NotFound();
        }


        [HttpPost("new-item")]
        public async Task<IActionResult> Create(ItemModel item)
        {
            var allItems = await _itemsService.GetAsync();

            if (allItems.Any(x => x.SerialNumber == item.SerialNumber) &&
                allItems.Any(x => x.Name.ToLower() == item.Name.ToLower()))
            {
                return BadRequest("Item with whis name and serial number already exists");
            }
            
            var qrCodeCoreValue = $"{item.Name}/{item.SerialNumber}";
            item.QrCode!.CoreValue = qrCodeCoreValue;

            //QrCodesController qrController = new();
            //var qr = qrController.GenerateQrCode(item.QrCode!.CoreValue);
            //item.QrCode.SvgFormat = qr!.ToString();
            
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

            return Ok(existingItem);
        }

    }
}
