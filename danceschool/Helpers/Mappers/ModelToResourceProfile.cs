using AutoMapper;
using danceschool.Models;

namespace danceschool.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            //TODO 
            //Update the mapping linq
            //https://docs.automapper.org/en/stable/Queryable-Extensions.html
            CreateMap<Course, CourseDTO>();
        }
    }
}