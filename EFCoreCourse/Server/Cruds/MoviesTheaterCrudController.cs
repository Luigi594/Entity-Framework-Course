using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EFCoreCourse.Entities.MovieTheater;

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
    }
}
