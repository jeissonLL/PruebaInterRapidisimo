using Application.DTO;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly  ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<User> AddUser(User CreateUser)
        {
            try
            {
                _context.Users.Add(CreateUser);
                await _context.SaveChangesAsync();
                return CreateUser;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al guardar el Usuario", ex);
            }
        }

        public async Task<User> Getid(int id)
        {
            try
            {
                var getUserId = await _context.Users
                    .Where(c => c.UserId == id)
                    .Select(c => new User
                    {
                        UserId = c.UserId,
                        Name = c.Name,
                        Email = c.Email,
                        RegistrationDate = c.RegistrationDate
                    })
                    .FirstOrDefaultAsync();

                return getUserId!;
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al guardar el usuario", ex);
            }
        }
    }
}
