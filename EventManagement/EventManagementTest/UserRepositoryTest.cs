using Application.Services;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UserRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        // 💻 Configuración de la base de datos en memoria
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _userRepository = new UserRepository(_context);

        // 👍 Limpiar la base de datos antes de cada prueba
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();

        // 📝🧠 Agregar datos iniciales
        _context.Users.Add(new User 
        { 
            UserId = 1, 
            Name = "John Doe", 
            Email = "john@example.com", 
            RegistrationDate = DateTime.UtcNow 
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddUser_Should_Add_User_To_Database()
    {
        // Arrange
        var newUser = new User
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            RegistrationDate = DateTime.UtcNow
        };

        // Act
        var result = await _userRepository.AddUser(newUser);
        var userInDb = await _context.Users.FindAsync(2);

        // Assert
        Assert.NotNull(userInDb);
        Assert.Equal(newUser.UserId, result.UserId);
        Assert.Equal("Jane Doe", result.Name);
        Assert.Equal("jane@example.com", result.Email);
    }

    [Fact]
    public async Task Getid_Should_Return_User_When_Found()
    {
        // Act
        var result = await _userRepository.Getid(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("john@example.com", result.Email);
    }

    [Fact]
    public async Task Getid_Should_Return_Null_When_User_Not_Found()
    {
        // Act
        var result = await _userRepository.Getid(99); // ❓ Usuario inexistente

        // Assert
        Assert.Null(result);
    }
}
