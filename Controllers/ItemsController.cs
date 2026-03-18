using Catalog.Service.Dtos;
using Catalog.Service.Domain;
using Catalog.Service.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController(InMemoryRepository repository) : ControllerBase
    {
        //private readonly InMemoryRepository _repository;
        // GET: api/<ItemsController>
        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetAll() 
            => Ok(repository.GetAll().Select(item => item.AsDto()));
        
        // GET /items/{id}
        [HttpGet("{id:guid}")]
        public ActionResult<ItemDto> GetById(Guid id)
        {
            var item = repository.GetById(id);
            return item is null
                ? NotFound()
                : Ok(item.AsDto());
        }
        
        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> Create(CreateItemDto dto)
        {
            if (dto.Price <= 0)
                return BadRequest(new { Error = "Price must be greater than zero." });
            
            var item = new Item
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.Create(item);
            return CreatedAtAction(nameof(GetById),
                new { id = item.Id },
                item.AsDto());
        }
        
        // PUT /items/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, UpdateItemDto dto)
        {
            if (dto.Price <= 0)
                return BadRequest(new { Error = "Price must be greater than zero." });
            
            var existing = repository.GetById(id);
            if (existing is null) return NotFound();
            var updated = new Item
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedDate = existing.CreatedDate
            };
            repository.Update(updated);
            return NoContent();
        }
        
        // DELETE /items/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var existing = repository.GetById(id);
            if (existing is null) return NotFound();
            repository.Delete(id);
            return NoContent();
        }
    }
}
