using BookAPI.Models;
using BookAPI.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Features.Commands
{
    public class GetBooks
    {
        public class Query : IRequest<IEnumerable<Book>> { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Book>>
        {
            private readonly IBookRepository _repository;

            public QueryHandler(IBookRepository repository) => _repository = repository;

            public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.Get();
            } 
        }
    }
}
