using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Login.Command
{
    public readonly record struct LoginCommand(LoginDTO LoginDTO) : IRequest<bool>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public LoginCommandHandler(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.LoginDTO);

            // Lógica de validación y autenticación (simplificada)
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);

            if (existingUser == null) return false;

            // Comparar contraseñas (esto debería ser con hash seguro)
            if (existingUser.Password != user.Password)
                return false;

            return true;
        }
    }
}
