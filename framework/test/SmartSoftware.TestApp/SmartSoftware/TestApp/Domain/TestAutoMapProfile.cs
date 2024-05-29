using AutoMapper;
using SmartSoftware.TestApp.Testing;

namespace SmartSoftware.TestApp.Domain;

public class TestAutoMapProfile : Profile
{
    public TestAutoMapProfile()
    {
        CreateMap<PersonEto, Person>().ReverseMap();

        CreateMap<Product, ProductCacheItem>();
    }
}
