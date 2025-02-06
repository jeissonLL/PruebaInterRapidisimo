﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Services.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Users.Querys
{
    public readonly record struct GetUserByIdQuery(int UserId) : IRequest<UserDTO>;

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            try
            {  
                var user = await _userRepository.Getid(request.UserId);

                return new UserDTO();
            }
            catch (Exception ex) 
            {
                throw new Exception("Error al consultar el usuario", ex);
            }
        }
    }

}
