using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Services.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EventDTO> Add(EventDTO CreateEvent)
        {
            var eventEntity = _mapper.Map<Event>(CreateEvent);
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDTO>(eventEntity);
        }
    }
}
