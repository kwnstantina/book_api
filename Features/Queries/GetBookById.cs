using BookAPI.Models;
using BookAPI.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Features.Commands
{
    public class GetBookById
    {
        public class Query : IRequest<Book>
        {
            public int Id { get; set; }
        }
        public class QueryHandler : IRequestHandler<Query, Book>
        {
            private readonly IBookRepository _repository;

            public QueryHandler(IBookRepository repository) => _repository = repository;

            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.Get(request.Id);
            }

        }
    }
}
