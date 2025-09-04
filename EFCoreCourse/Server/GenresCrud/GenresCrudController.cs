using EFCoreCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse.Server.GenresCrud
{
    public class GenresCrudController
    {
        public class GetGenres
        {
            public class GetGenresQuery : IRequest<List<Genres>>
            {

            }
            public class Handler(ApplicationDbContext context) : IRequestHandler<GetGenresQuery, List<Genres>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<List<Genres>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
                {
                    return await _context.Genres
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                }
            }
        }


        public class GetGenresByDescription
        {
            public class GetGenresByDescriptionQuery : IRequest<List<Genres>>
            {
                public string Description { get; set; }
            }
            public class Handler(ApplicationDbContext context) : IRequestHandler<GetGenresByDescriptionQuery, List<Genres>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<List<Genres>> Handle(GetGenresByDescriptionQuery request, CancellationToken cancellationToken)
                {
                    var resp = await _context.Genres
                        .Where(g => g.Description.Contains(request.Description))
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                    
                    if(resp.Count == 0)
                    {
                        throw new Exception($"No movie genres were found with the following description: {request.Description}");
                    }

                    return resp;
                }
            }
        }
    }
}
