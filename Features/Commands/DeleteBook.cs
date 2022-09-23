using BookAPI.Models;
using BookAPI.Repositories;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Features.Commands
{
    public class DeleteBook
    {

        public class Command : IRequest {
        
            public int Id { get; set; }
        
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0);
            }
        }

        public class QueryHandler : IRequestHandler<Command, Unit>
        {
            private readonly IBookRepository _repository;

            public QueryHandler(IBookRepository repository) => _repository = repository;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
            
                return await _repository.Delete(request.Id,cancellationToken);
            }
        }
    }
}
