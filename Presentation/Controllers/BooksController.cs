using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _serviceManager.BookService.GetAllBooks(false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = _serviceManager.BookService.GetOneBook(false, id);

            return Ok(book);
        }

        [HttpPost]
        public IActionResult InsertOneBook([FromBody] Book book)
        {
            _serviceManager.BookService.InsertOneBook(book);
            return Ok(book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            _serviceManager.BookService.UpdateOneBook(id, book);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveOneBook([FromRoute(Name = "id")] int id)
        {
            var entity = _serviceManager.BookService.GetOneBook(true, id);
            return NoContent();
        }
    }

}
