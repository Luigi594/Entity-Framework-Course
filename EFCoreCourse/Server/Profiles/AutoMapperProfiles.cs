using AutoMapper;
using EFCoreCourse.Entities;
using static EFCoreCourse.Entities.Actors;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Server.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            #region Actors DTO

            CreateMap<Actors, ActorsDTO>();

            #endregion


            #region MovieTheater DTO

            CreateMap<MovieTheater, MoviesTheaterDTO>()
                .ForMember(dto => dto.Latitude, entity => entity.MapFrom(prop => prop.Location.Y))
                .ForMember(dto => dto.Longitude, entity => entity.MapFrom(prop => prop.Location.X));

            #endregion

        }
    }
}
