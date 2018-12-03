using System;
using studentsApi.Controllers.Resources;
using studentsApi.Core.Models;
using AutoMapper;

namespace studentsApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Student, StudentResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            //API Resource to Domain
            CreateMap<StudentResource, Student>();
            CreateMap<StudentQueryResource, StudentQuery>();
        }
    }
}