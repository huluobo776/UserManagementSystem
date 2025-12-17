using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();//
        }

    }
}
