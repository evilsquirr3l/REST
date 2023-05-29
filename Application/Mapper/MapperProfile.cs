using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        
        CreateMap<BaseCategory, Category>();
        CreateMap<BaseItem, Item>();
        CreateMap<BaseItem, ItemDto>().ReverseMap();
    }
}
