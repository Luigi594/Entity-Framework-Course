using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using EFCoreCourse.Entities.Enums;
using EFCoreCourse.Utils;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EFCoreCourse.Entities.MovieTheater;
using static EFCoreCourse.Server.Cruds.GenresCrudController.CreateGenre;

namespace EFCoreCourse.Server.Cruds
{
    public class MoviesTheaterCrudController
    {
        public class GetMoviesTheater
        {
            public class GetMoviesTheaterQuery : IRequest<List<MoviesTheaterDTO>>
            {

            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetMoviesTheaterQuery, List<MoviesTheaterDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<MoviesTheaterDTO>> Handle(GetMoviesTheaterQuery request, CancellationToken cancellationToken)
                {
                    var moviesTheater = await _context.MovieTheater
                        .ProjectTo<MoviesTheaterDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return moviesTheater;
                }
            }
        }


        public class GetMoviesTheaterNearBy
        {
            public class GetMoviesTheaterNearByQuery : IRequest<List<MoviesTheaterDTO>>
            {
                public double Latitude { get; set; }
                public double Longitude { get; set; }
            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetMoviesTheaterNearByQuery, List<MoviesTheaterDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<MoviesTheaterDTO>> Handle(GetMoviesTheaterNearByQuery request, CancellationToken cancellationToken)
                {
                    var geomemtryFactory = new GeometryFactory();
                    var userLocation = geomemtryFactory.GetGeometryFromLocation(request.Latitude, request.Longitude);

                    var maximumDistanceInMeters = 0;

                    var moviesTheater = await _context.MovieTheater
                        .OrderBy(x => x.Location.Distance(userLocation))
                        .Where(x => x.Location.IsWithinDistance(userLocation, maximumDistanceInMeters))
                        .ProjectTo<MoviesTheaterDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return moviesTheater;
                }
            }
        }

        public class CreateMovieTheather
        {
            public class CreateMovieTheatherCommand: IRequest<ActionResult<Response>>
            {
                public string Name { get; set; }
                public double Latitude { get; set; }
                public double Longitude { get; set; }
                public MovieOfferVm MovieOffer { get; set; }
                public List<MovieTheaterRoomVm> MovieTheaterRooms { get; set; }

                public class MovieOfferVm
                {
                    public decimal DiscountPercentage { get; set; }
                    public DateTime StartDate { get; set; }
                    public DateTime EndDate { get; set; }
                }

                public class MovieTheaterRoomVm
                {
                    public decimal Price { get; set; }
                    public MovieTheaterRoomType MovieTheaterRoomType { get; set; }
                }
            }

            public class Validator : AbstractValidator<CreateMovieTheatherCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("The Name is required.")
                        .MaximumLength(100).WithMessage("The Name must not exceed 100 characters.");

                    RuleFor(x => x.MovieOffer.DiscountPercentage)
                        .InclusiveBetween(1,100)
                        .When(x => x.MovieOffer != null)
                        .WithMessage("The discount percentage must be between 1 and 100.");
                }
            }

            public class Response
            {
                public string Message { get; set; }

                public static Response New(string message)
                {
                    return new Response
                    {
                        Message = message
                    };
                }
            }

            public class CreateMovieTheatherCommandHandler(ApplicationDbContext context)
                :IRequestHandler<CreateMovieTheatherCommand, ActionResult<Response>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<Response>> Handle(CreateMovieTheatherCommand command, CancellationToken cancellationToken)
                {
                    var movieTheater = await _context.MovieTheater
                        .FirstOrDefaultAsync(x => x.Name == command.Name, cancellationToken);

                    if(movieTheater != null)
                        throw new Exception($"A movie theater with the name {command.Name} already exists.");

                    Response response = null;

                    try
                    {
                        if (command.MovieOffer != null)
                        {
                            var newMovieOffer = MovieOffer.Create(command.MovieOffer.DiscountPercentage,
                                command.MovieOffer.StartDate, command.MovieOffer.EndDate);

                            #region First Idea

                            //var rooms = new List<MovieTheaterRoom>();

                            //if (command.MovieTheaterRooms != null && command.MovieTheaterRooms.Count > 0)
                            //{
                            //    foreach (var room in command.MovieTheaterRooms)
                            //    {
                            //        // I could validate each room here, throw errors, etc.
                            //        var newRoom = MovieTheaterRoom.Create(room.Price, room.MovieTheaterRoomType);
                            //        rooms.Add(newRoom);
                            //    }
                            //}

                            #endregion

                            var newMovieTheaterRooms = command.MovieTheaterRooms.Select(x =>
                                MovieTheaterRoom.Create(x.Price, x.MovieTheaterRoomType)).ToList();

                            var newMovieTheater = MovieTheater.Create(command.Name, command.Latitude,
                                command.Longitude, newMovieTheaterRooms);

                            foreach (var room in newMovieTheaterRooms)
                            {
                                room.MovieTheaterId = newMovieTheater.Id;
                            }

                            newMovieOffer.MovieTheaterId = newMovieTheater.Id;

                            await _context.MovieTheater.AddAsync(newMovieTheater, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);

                            response = Response.New($"New Movie Theater: {newMovieTheater.Name} added successfully!");

                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"ERROR: {ex.Message}");
                    }

                    return response;
                }
            }
        }
    }
}
