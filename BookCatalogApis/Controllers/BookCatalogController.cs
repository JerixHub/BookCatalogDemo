using BookCatalogApis.Data;
using BookCatalogApis.DTO;
using BookCatalogApis.Entity;
using BookCatalogApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookCatalogApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCatalogController : ControllerBase
    {
        private readonly BookCatalogContext _context;

        public BookCatalogController(BookCatalogContext context)
        {
            _context = context;
        }

        // GET: api/<BookCatalogController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page = null, int? pageSize = null)
        {
            if (!pageSize.HasValue && !page.HasValue)
            {
                var books = await _context.Books.ToListAsync();
                var bookDtos = books.Select(b => b.AsDto()).ToList();
                return Ok(bookDtos);
            }

            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            int skip = (page.Value - 1) * pageSize.Value;

            var paginatedBooks = await _context.Books.Skip(skip).Take(pageSize.Value).ToListAsync();

            var paginatedBookDtos = paginatedBooks.Select(b => b.AsDto()).ToList();
            return Ok(paginatedBookDtos);
        }

        // GET api/<BookCatalogController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var bookDto = book?.AsDto();

            return bookDto == null ? NotFound() : Ok(bookDto);
        }

        // POST api/<BookCatalogController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(CreateBookDTO createBookDTO)
        {
            Book book = new()
            {
                CategoryId = createBookDTO.CategoryId,
                Title = createBookDTO.Title,
                Description = createBookDTO.Description,
                PublishDateUtc = createBookDTO.PublishDateUtc
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        // PUT api/<BookCatalogController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, UpdateBookDTO updateBookDTO)
        {
            _context.Entry(updateBookDTO).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<BookCatalogController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            if(bookToDelete == null) return NotFound();

            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
