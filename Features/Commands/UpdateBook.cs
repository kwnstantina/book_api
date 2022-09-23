using BookAPI.Models;
using BookAPI.Repositories;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Features.Commands
{
    public class UpdateBook
    {

        public class Command : IRequest<int>
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
       
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0); 
                RuleFor(c => c.Title).NotEmpty().MaximumLength(150);
                RuleFor(c => c.Author).NotEmpty().MaximumLength(100);
                RuleFor(c =>c.Description).NotEmpty().MaximumLength(150);
            }
        }

        public class QueryHandler : IRequestHandler<Command, int>
        {
            private readonly IBookRepository _repository;

            public QueryHandler(IBookRepository repository) => _repository = repository;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Book
                {
                    Id = request.Id,
                    Title = request.Title,
                    Author = request.Author,
                    Description = request.Description
                };
                await _repository.Update(entity, cancellationToken);
                return entity.Id;
            }
        }
    }
}
