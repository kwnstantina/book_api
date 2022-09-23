using Ardalis.GuardClauses;
using BookAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public Task<IEnumerable<Book>> Create()
        {
            throw new NotImplementedException();
        }

        public async Task<Unit> Delete(int id, CancellationToken cancellationToken)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            // c# guard clauses
            //  if (bookToDelete == null) return Unit.Value;
            // ardalis gurd clauses
            Guard.Against.Null(bookToDelete, nameof(bookToDelete));
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task Update(Book book, CancellationToken cancellationToken)
        {
            Guard.Against.Null(_context.Books, nameof(book));
            var bookToDelete = await _context.Books.FindAsync(book.Id);
            _context.Books.Update(bookToDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }

    
    }
}
