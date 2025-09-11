using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EFCoreCourse.Entities.Actors;
using static EFCoreCourse.Entities.Movie;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Server.Cruds
{
    public class MoviesCrudController
    {
        public class GetMovies
        {
            public class GetMoviesQuery : IRequest<List<MovieDTO>>
            {

            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetMoviesQuery, List<MovieDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<MovieDTO>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
                {
                    #region Tutorial Version
                    //var movies = await _context.Movie
                    //    .Include(x => x.Genres.OrderByDescending(w => w.Description))
                    //    .Include(x => x.MovieTheaterRooms)
                    //        .ThenInclude(s => s.MovieTheater)
                    //    .Include(x => x.MoviesActors)
                    //        .ThenInclude(w => w.Actor)
                    //    .ToListAsync(cancellationToken);

                    //var moviesDto = _mapper.Map<List<MovieDTO>>(movies);

                    #endregion

                    #region Consultas

                    var movies = await _context.Movie
                        .Include(x => x.Genres)
                        .ToListAsync(cancellationToken);

                    var moviesIds = movies.Select(x => x.Id).Distinct().ToList();

                    var movieTheaters = await _context.MovieTheaterRoom
                        .Include(x => x.MovieTheater)
                        .Where(x => x.Movies.Any(s => moviesIds.Contains(s.Id)))
                        .ToListAsync(cancellationToken);

                    var actors = await _context.MoviesActors
                        .Include(x => x.Actor)
                        .Where(x => moviesIds.Contains(x.MovieId))
                        .ToListAsync(cancellationToken);

                    #endregion

                    List<MovieDTO> moviesResult = new List<MovieDTO>();

                    foreach (var movie in movies)
                    {
                        var movieTheatersDto = movieTheaters.Where(x => x.Movies.Any(s => s.Id == movie.Id))
                            .Select(x => x.MovieTheater)
                            .ToList();

                        var actorsDto = actors.Where(x => x.MovieId == movie.Id).ToList();

                        var movieDto = _mapper.Map<MovieDTO>(movie);

                        movieDto.Genres = movieDto.Genres
                            .GroupBy(g => g.Id)
                            .Select(g => g.First())
                            .ToList();

                        movieDto.MoviesTheaters = _mapper.Map<List<MoviesTheaterDTO>>(movieTheatersDto);
                        movieDto.Actors = _mapper.Map<List<ActorsDTO>>(actorsDto);

                        moviesResult.Add(movieDto);
                    }

                    return moviesResult;
                }
            }
        }
    }
}
