using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using System;
using System.Linq;  


namespace Play.Catalog.Service.Controllers
{
    //https://localhost:5001/items
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals small amount of damage", 20, DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        //GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = items.Where(item => item.id == id).SingleOrDefault();

            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        //return ActionResult that could be a 200 OK
        public ActionResult<ItemDto> Post(CreateItemDto createdItem)
        {
            var item = new ItemDto(Guid.NewGuid(), createdItem.name, createdItem.description, createdItem.price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(item => item.id == id).SingleOrDefault();
            
            var updatedItem = existingItem with 
            {
                name = updateItemDto.name,
                description = updateItemDto.description,
                price = updateItemDto.price
            };

            var index = items.FindIndex(existingItem => existingItem.id == id);
            items[index] = updatedItem;

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.id == id);
            items.RemoveAt(index);

            return NoContent();
        }
    }
}