using AutoMapper;
using PostGreSqlTransaction.DTOs;
using PostGreSqlTransaction.Entities;

namespace PostGreSqlTransaction.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Account, AccountDTO>().ReverseMap();
        CreateMap<CreateUserDTO, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>();
    }
}