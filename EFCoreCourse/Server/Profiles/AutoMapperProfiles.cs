using AutoMapper;
using EFCoreCourse.Entities;
using static EFCoreCourse.Entities.Actors;
using static EFCoreCourse.Entities.GenresDTO;
using static EFCoreCourse.Entities.Movie;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Server.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Actors DTO

            CreateMap<Actors, ActorsDTO>();

            #endregion

            #region Genre DTO

            CreateMap<GenresDTO, GenreDTO>();

            #endregion

            #region MovieTheater DTO

            CreateMap<MovieTheater, MoviesTheaterDTO>()
                .ForMember(dto => dto.Latitude, entity => entity.MapFrom(prop => prop.Location.Y))
                .ForMember(dto => dto.Longitude, entity => entity.MapFrom(prop => prop.Location.X));

            #endregion

            #region Movie DTO

            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.MoviesTheaters, entity => entity.MapFrom(prop => prop.MovieTheaterRooms.Select(x => x.MovieTheater)))
                .ForMember(dto => dto.Actors, entity => entity.MapFrom(prop => prop.MoviesActors.Select(s => s.Actor)));

            #endregion
        }
    }
}
