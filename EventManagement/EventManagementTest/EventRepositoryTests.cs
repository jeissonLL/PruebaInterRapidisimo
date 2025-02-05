using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.DTO;
using Infraestructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

public class EventRepositoryTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly Mock<IMapper> _mapperMock;
    private readonly EventRepository _eventRepository;

    public EventRepositoryTests()
    {
        // Configurar la base de datos en memoria
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);

        // Crear un mock de AutoMapper
        _mapperMock = new Mock<IMapper>();

        // Inicializar el repositorio con la BD en memoria y el mock de AutoMapper
        _eventRepository = new EventRepository(_dbContext, _mapperMock.Object);
    }

    [Fact]
    public async Task Add_ShouldAddEventSuccessfully()
    {
        // Arrange: Datos de prueba
        var eventDto = new EventDTO { 
            Name = "Test Event", 
            Description = "Test Descrption", 
            DateTime = DateTime.UtcNow, 
            Location = "Test Location", 
            MaxCapacity = 500, 
            CreatedByUserId = 1011 
        };

        var eventEntity = new Event { 
            EventId = 1, 
            Name = "Test Event", 
            Description = "Test Descrption", 
            DateTime = DateTime.UtcNow, 
            Location = "Test Location", 
            MaxCapacity = 500, 
            CreatedByUserId = 1011 
        };

        // Configurar el mock de AutoMapper
        _mapperMock.Setup(m => m.Map<Event>(eventDto)).Returns(eventEntity);
        _mapperMock.Setup(m => m.Map<EventDTO>(eventEntity)).Returns(eventDto);

        // Act: Llamar al método Add
        var result = await _eventRepository.Add(eventDto);

        // Assert: Validar resultados
        Assert.NotNull(result);
        Assert.Equal(eventDto.Name, result.Name);
        Assert.Equal(eventDto.Location, result.Location);

        // Verificar que el evento fue guardado en la BD
        var savedEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.EventId == eventEntity.EventId);
        Assert.NotNull(savedEvent);
        Assert.Equal(eventEntity.Name, savedEvent.Name);
    }
}
