using Entities;
using Entities.Dtos.Book;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Filters;
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

        [BookRequestParameterStateFilter(Order =int.MinValue)]
        [ValidationFilter(Order = int.MinValue + 1)]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery]BookRequestParameters requestParameters)
        {
            var pagedList = await _serviceManager.BookService.GetAllBooksAsync(false,requestParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.metaData));

            return Ok(pagedList.books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
        {
            var book = await _serviceManager.BookService.GetOneBookAsync(false, id);

            return Ok(book);
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> InsertOneBook([FromBody] BookDtoForInsert bookDto)
        {
            await _serviceManager.BookService.InsertOneBookAsync(bookDto);
            return NoContent();
        }

        [ValidationFilter]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            await _serviceManager.BookService.UpdateOneBookAsync(id, bookDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveOneBook([FromRoute(Name = "id")] int id)
        {
            await _serviceManager.BookService.DeleteOneBookAsync(id);
            return NoContent();
        }
    }

}
