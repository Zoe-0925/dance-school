using AutoMapper;

namespace danceschool.Helpers.Mappers
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());

    }
}


