using AutoMapper;

namespace Zamin.Extentions.ObjectMappers.AutoMapper.Sample.Models;

public class PersonStudentAutoMapperProfile : Profile
{
    public PersonStudentAutoMapperProfile()
    {
        CreateMap<Person, Student>().ReverseMap();
    }
}
