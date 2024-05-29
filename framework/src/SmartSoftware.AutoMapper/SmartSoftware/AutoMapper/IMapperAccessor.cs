using AutoMapper;

namespace SmartSoftware.AutoMapper;

public interface IMapperAccessor
{
    IMapper Mapper { get; }
}
