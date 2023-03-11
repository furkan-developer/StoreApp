using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Models.Repository;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks(){
            try
            {    
                var books = _context.Books.ToList();
                return Ok(books);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,$"Insert faild: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name ="id")]int id){
            var book = _context.Books.Where(b=> b.Id.Equals(id)).SingleOrDefault();

            if(book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult InsertOneBook([FromBody]Book book){
            if(book is null)
                return BadRequest();

            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                return Ok(book);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,$"Insert faild: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name ="id")]int id,[FromBody]Book book)
        {
            try
            {
                var entity = _context.Books.Where(b=> b.Id.Equals(id)).FirstOrDefault();

                if(entity is null)
                    return NotFound();

                if(!entity.Id.Equals(book.Id))
                    return BadRequest();

                var entityEntry = _context.Entry(book);
                entityEntry.State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }   

        [HttpDelete("{id:int}")]
        public IActionResult RemoveOneBook([FromRoute(Name ="id")]int id)
        {   
            try
            {
                var entity = _context.Books.SingleOrDefault(b=> b.Id.Equals(id));

                if(entity is null)
                    return NotFound();

                _context.Books.Remove(entity);
                _context.SaveChanges();
                
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
                 // TODO
            }
        } 

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name ="id")]int id,[FromBody]JsonPatchDocument<Book> patchBook)
        {
            if(patchBook is null)
                return BadRequest();

            try
            {
                var entity = _context.Books.Where(b=> b.Id.Equals(id)).SingleOrDefault();

                if(entity is null)
                    return NotFound();

                patchBook.ApplyTo(entity);
                _context.SaveChanges();

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
    }
}