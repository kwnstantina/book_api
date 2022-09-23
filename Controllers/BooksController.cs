using Ardalis.GuardClauses;
using BookAPI.Features.Commands;
using BookAPI.Models;
using BookAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //OLD CODE
       // private readonly IBookRepository _bookRepository;
        private readonly IMediator _mediator;


        //OLD CODE 
        /*public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }*/


        //REFACTORED CODE USING LAMDBA OPERATOR
        public BooksController(IMediator mediator) => _mediator = mediator;


        //OLD CODE
        // [HttpGet]
        // public async Task<IEnumerable<Book>> GetBooks() =>
        // {
        //   return await _bookRepository.Get();
        // }


        //REFACTORED CODE USING LAMDBA OPERATOR
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks() => await _mediator.Send(new GetBooks.Query());


        // OLD CODE
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Book>> GetBooks(int id)
        //      //{
        //          return await _bookRepository.Get(id);
        //}


        //REFACTORED CODE USING LAMDBA OPERATOR
        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id) => await _mediator.Send(new GetBookById.Query{ Id = id });

        // OLD CODE
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        //{
        //    var newBook = await _bookRepository.Create(book);
        //    return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        //}

        //REFACTORED CODE
        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] PostBook.Command command)
        {
          var postedBook=  await _mediator.Send(new PostBook.Command() {Title= command.Title,Author= command.Author, Description=command.Description });
          return CreatedAtAction(nameof(GetBook), new { id = postedBook }, null);
        }

        //OLD CODE 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteBook(int id)
        //{
        //    var bookToDelete = await _bookRepository.Get(id);
        //    if (bookToDelete == null)
        //        return NotFound();

        //    await _bookRepository.Delete(bookToDelete.Id);
        //    return NoContent();
        //}


        //REFACTOED CODE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _mediator.Send(new DeleteBook.Command { Id = id });
            return NoContent();
        }

        //OLD CODE 
        /* [HttpPut]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
           if (id != book.Id)
           {
              return BadRequest();
           }
            await _bookRepository.Update(book);
            return NoContent();
        }*/

        //REFACTORED CODE 
        [HttpPut]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
            /* if (id != book.Id)
             {
                 return BadRequest();
             }*/
            Guard.Against.Null(id, nameof(id));
            await _mediator.Send(new UpdateBook.Command {Id=book.Id,Title = book.Title, Author = book.Author, Description = book.Description });
            return NoContent();
        }

    }  
}
