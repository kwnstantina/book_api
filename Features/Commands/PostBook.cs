using BookAPI.Models;
using BookAPI.Models.DTO.BooksDTO;
using BookAPI.Repositories;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookAPI.Features.Commands
{
    public class PostBook
    {

        public class Command : IRequest<int>
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title).NotEmpty().MaximumLength(150);
                RuleFor(c => c.Author).NotEmpty().MaximumLength(100);
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
                    Title = request.Title,
                    Author = request.Author,
                    Description = request.Description
                };
                await _repository.Create(entity);

                return entity.Id;
            }
        }
    }
}
