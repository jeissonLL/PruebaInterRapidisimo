using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IEventRepository
    {
        Task<EventDTO> Add(EventDTO CreateEvent);
    }
}
