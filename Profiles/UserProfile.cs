using AutoMapper;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.DTOs.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();
    }
}
