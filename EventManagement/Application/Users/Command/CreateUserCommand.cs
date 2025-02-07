using Application.DTO;
using Application.Services.Interfaces;
using MediatR;
using Domain.Entities;

namespace Application.Users.Command
{
    public readonly record struct CreateUserCommand (
        string Name,
        string Email,
        string Password,
        DateTime RegistrationDate = default) : IRequest<UserDTO>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                RegistrationDate = request.RegistrationDate
            };

            await _userRepository.AddUser(user);

            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name, 
                Email = user.Email,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate
            };
        }
    }
}
