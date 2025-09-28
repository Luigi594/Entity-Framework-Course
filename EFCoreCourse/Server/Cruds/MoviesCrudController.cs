using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
                        .Include(x=>x.MovieTheaterRooms)
                            .ThenInclude(x=>x.MovieTheater)
                        .ToListAsync(cancellationToken);

                    var moviesIds = movies.Select(x => x.Id).Distinct().ToList();

                    var actors = await _context.MoviesActors
                        .Include(x => x.Actor)
                        .Where(x => moviesIds.Contains(x.MovieId))
                        .ToListAsync(cancellationToken);

                    #endregion

                    List<MovieDTO> moviesResult = new List<MovieDTO>();

                    foreach (var movie in movies)
                    {
                        var movieTheatersDto = movie.MovieTheaterRooms
                            .Where(x => x.Movies.Any(s => s.Id == movie.Id))
                            .Select(x => x.MovieTheater).Distinct()
                            .ToList();

                        var actorsDto = actors.Where(x => x.MovieId == movie.Id).Select(x => x.Actor).ToList();

                        var movieDto = _mapper.Map<MovieDTO>(movie);

                        movieDto.MoviesTheaters = _mapper.Map<List<MoviesTheaterDTO>>(movieTheatersDto);
                        movieDto.Actors = _mapper.Map<List<ActorsDTO>>(actorsDto);

                        moviesResult.Add(movieDto);
                    }

                    return moviesResult;
                }
            }
        }

        public class GetMoviesGroupedByIsOnDisplay
        {
            public class GetMoviesGroupedByIsOnDisplayQuery : IRequest<List<Response>>
            {

            }

            public class Response
            {
                public bool IsOnDisplay { get; set; }
                public decimal Count { get; set; }
                public List<MovieDTOBase> Movies { get; set; }
            }

            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetMoviesGroupedByIsOnDisplayQuery, List<Response>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<Response>> Handle(GetMoviesGroupedByIsOnDisplayQuery request, CancellationToken cancellationToken)
                {
                    var groupedMovies = await _context.Movie
                        .ProjectTo<MovieDTOBase>(_mapper.ConfigurationProvider) 
                        .GroupBy(dto => dto.IsOnDisplay)                        
                        .Select(group => new Response
                        {
                            IsOnDisplay = group.Key,
                            Count = group.Count(),
                            Movies = group.ToList()
                        })
                        .ToListAsync(cancellationToken);

                    return groupedMovies;

                }
            }
        }

        public class CreateNewMovie
        {
            public class CreateNewMovieCommand: IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string Title { get; set; }
                public DateTime ReleaseDate { get; set; }
                public string PosterUrl { get; set; }
                public List<Guid> GenresIds { get; set; }   
                public List<Guid> MovieTheaterRoomsIds { get; set; }
                public List<ActorVm> Actors { get; set; }

                public class ActorVm
                {
                    public Guid Id { get; set; }
                    public string Character { get; set; }
                }
            }

            public class CreateNewMovieCommandHandler(ApplicationDbContext context, IMapper mapper)
                : IRequestHandler<CreateNewMovieCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateNewMovieCommand command, CancellationToken cancellationToken)
                {
                    var genresIds= command.GenresIds.Distinct().ToList();
                    var movieTheaterRoomsIds = command.MovieTheaterRoomsIds.Distinct().ToList();

                    #region Genres and MovieTheaterRooms Validation
                    var genres = await _context.Genres
                        .Where(x => genresIds.Contains(x.Id))
                        .ToListAsync(cancellationToken);
                    
                    for (int i = 0; i < genres.Count; i++)
                    {
                        var genre = genres.ElementAt(i);
                        var genreExists = genres.Where(x => x.Id == genre.Id).FirstOrDefault() 
                            ?? throw new Exception($"The genre with id {genre.Id} does not exist.");
                    }

                    var movieTheaterRooms = await _context.MovieTheaterRoom
                        .Where(x => movieTheaterRoomsIds.Contains(x.Id))
                        .ToListAsync(cancellationToken);

                    for (int i = 0; i < movieTheaterRooms.Count; i++)
                    {
                        var movieTheaterRoom = movieTheaterRooms.ElementAt(i);  
                        var movieTheaterRoomExists = movieTheaterRooms
                            .Where(x => x.Id == movieTheaterRoom.Id)
                            .FirstOrDefault() 
                            ?? throw new Exception($"The movie theater room with id {movieTheaterRoom.Id} does not exist.");
                    }

                    #endregion

                    var message = "";

                    if(command.Actors != null && command.Actors.Count > 0)
                    {
                        var actorsIds = command.Actors.Select(x => x.Id).Distinct().ToList();
                        var actors = await _context.Actors
                            .Where(x => actorsIds.Contains(x.Id))
                            .ProjectTo<ActorsDTO>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        var existingMovie = await _context.Movie
                            .FirstOrDefaultAsync(x => x.Title == command.Title, cancellationToken);

                        if (existingMovie != null)
                            throw new Exception($"A movie with the title {command.Title} already exists.");

                        var newMovie = Movie.Create(command.Title, command.ReleaseDate, true, command.PosterUrl);

                        var moviesActors = new List<MoviesActors>();
                        int order = 1;

                        foreach(var actorVm in command.Actors)
                        {
                            var actor = actors
                                .Where(x => x.Id == actorVm.Id).FirstOrDefault() 
                                ?? throw new Exception($"The actor with id {actorVm.Id} does not exist.");

                            var newMovieActor = MoviesActors.Create(newMovie.Id, actor.Id, actorVm.Character, order++);
                            moviesActors.Add(newMovieActor);
                        }

                        newMovie.MoviesActors = moviesActors;

                        genres.ForEach(genre => newMovie.Genres.Add(genre));
                        movieTheaterRooms.ForEach(room => newMovie.MovieTheaterRooms.Add(room));

                        await _context.Movie.AddAsync(newMovie, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        message = $"The movie {newMovie.Title} has been created successfully."; 
                    }

                    return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                }
            }
        }
    }
}
