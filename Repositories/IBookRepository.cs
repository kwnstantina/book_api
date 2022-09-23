using BookAPI.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookAPI.Models.DTO.BooksDTO;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();
        Task<Book> Get(int id);
        Task<int> Create(Book book);
        Task Update(Book book,CancellationToken cancellationToken);
        Task<Unit> Delete(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> Create();
    }
}
