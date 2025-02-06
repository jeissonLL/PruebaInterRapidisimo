using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User CreateUser);
        Task<User> Getid(int id);
    }
}
