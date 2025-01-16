using Application.Login.Command;
using Application.Users.Command;
using Infraestructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Application.Mappings;
using Application.Events.Command;
using Application.EventUser.Command;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper (esto escanea el ensamblado de tu Application para buscar clases de mapeo)
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Register DbContext with the connection string
var connectionString = builder.Configuration.GetConnectionString("Conexion");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString!));

// Register MediatR and scan for handlers in the assembly
builder.Services.AddMediatR(typeof(CreateUserCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateEventCommand).Assembly);
builder.Services.AddMediatR(typeof(CreateEventUserCommand).Assembly);
builder.Services.AddMediatR(typeof(LoginCommand).Assembly);

//cors
builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app => {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
