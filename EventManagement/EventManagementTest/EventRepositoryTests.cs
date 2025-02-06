using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class EventRepositoryTests
{
    private readonly EventRepository _eventRepository;
    private readonly ApplicationDbContext _dbContext;

    public EventRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _eventRepository = new EventRepository(_dbContext);
    }

    [Fact]
    public async Task Add_Should_Add_Event_And_Return_Entity()
    {
        // Arrange
        var newEvent = new Event
        {
            Name = "Tech Conference",
            Description = "Evento sobre tecnología",
            DateTime = DateTime.UtcNow,
            Location = "Bogotá, Colombia",
            MaxCapacity = 1000,
            CreatedByUserId = 1
        };

        // Act
        var result = await _eventRepository.Add(newEvent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newEvent.Name, result.Name);
        Assert.Equal(newEvent.Description, result.Description);
        Assert.Equal(newEvent.MaxCapacity, result.MaxCapacity);

        var eventInDb = await _dbContext.Events.FindAsync(result.EventId);
        Assert.NotNull(eventInDb);
    }

    [Fact]
    public async Task Add_Should_Throw_Exception_When_SaveChanges_Fails()
    {
        // Arrange
        var newEvent = new Event
        {
            Name = "Tech Conference",
            Description = "Evento sobre tecnología",
            DateTime = DateTime.UtcNow,
            Location = "Bogotá, Colombia",
            MaxCapacity = 1000,
            CreatedByUserId = 1
        };

        _dbContext.Dispose(); // Simula un error en SaveChangesAsync

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _eventRepository.Add(newEvent));
        Assert.Contains("Error al guardar el evento", exception.Message);
    }
}
