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
            var allItems = await _itemsService.GetAsync();
            var itemType = await _itemTypesService.GetAsync(item.ItemType.Name);
            var provider = await _providersService.GetAsync(item.Provider.EdrpouCode);

            if (allItems.Any(x => x.SerialNumber == item.SerialNumber) &&
                allItems.Any(x => x.Name.ToLower() == item.Name.ToLower()))
            {
                return BadRequest();
            }
            
            if (itemType is null)
            {
                await _itemTypesService.CreateAsync(item.ItemType);
                
            }
            if (provider is null)
            {
                await _providersService.CreateAsync(item.Provider);
            }

            var qrCodeCoreValue = $"{item.Name}/{item.SerialNumber}";
            item.QrCode!.CoreValue = qrCodeCoreValue;

            QrCodesController qrController = new();
            var qr = qrController.GenerateQrCode(item.QrCode!.CoreValue);
            item.QrCode.SvgFormat = qr!.ToString();

            var existingItemType = await _itemTypesService.GetAsync(item.ItemType.Name);
            item.ItemType.Id = existingItemType.Id;

            var existingProvider = await _providersService.GetAsync(item.Provider.EdrpouCode);
            item.Provider.Id = existingProvider.Id;

            await _itemsService.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }
        

        [HttpPut("update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ItemModel item)
        {
            var itemType = _itemTypesService.GetAsync(item.ItemType.Name);
            var provider = _providersService.GetAsync(item.Provider.EdrpouCode);

            var existingItem = await _itemsService.GetAsync(id);

            if (existingItem is null)
                return BadRequest();

            item.Id = existingItem.Id;

            if (itemType.Result is null)
            {
                
            }

            var existingItemType = await _itemTypesService.GetAsync(item.ItemType.Name);
            item.ItemType.Id = existingItemType.Id;

            var existingProvider = await _providersService.GetAsync(item.Provider.EdrpouCode);
            item.Provider.Id = existingProvider.Id;

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
