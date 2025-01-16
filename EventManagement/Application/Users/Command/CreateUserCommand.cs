using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command
{
    public readonly record struct CreateUserCommand (
        string Name,
        string Email,
        string Password,
        DateTime RegistrationDate = default) : IRequest<string>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly ApplicationDbContext _context;

        public CreateUserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            _context.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user?.ToString() ?? string.Empty;
        }
    }
}
