using Application.Services;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class EventRepositoryTests
{
    private readonly EventRepository _eventRepository;
    private readonly ApplicationDbContext _dbContext;

    const string _Name = "Tech Conference";
    const string _Description = "Evento sobre tecnología";
    const string _Location = "Bogotá, Colombia";
    const int _MaxCapacity = 1000;
    const int _CreatedByUserId = 1;

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
            Name = _Name,
            Description = _Description,
            DateTime = DateTime.UtcNow,
            Location = _Location,
            MaxCapacity = _MaxCapacity,
            CreatedByUserId = _CreatedByUserId
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
            Name = _Name,
            Description = _Description,
            DateTime = DateTime.UtcNow,
            Location = _Location,
            MaxCapacity = _MaxCapacity,
            CreatedByUserId = _CreatedByUserId
        };

        _dbContext.Dispose(); // Simula un error en SaveChangesAsync

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _eventRepository.Add(newEvent));
        Assert.Contains("Error al guardar el evento", exception.Message);
    }
}
