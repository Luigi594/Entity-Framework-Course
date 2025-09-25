using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse.Server.Cruds
{
    public class GenresCrudController
    {
        public class GetGenres
        {
            public class GetGenresQuery : IRequest<List<Genres.GenreDTO>>
            {

            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetGenresQuery, List<Genres.GenreDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;


                public async Task<List<Genres.GenreDTO>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
                {
                    var genres = await _context.Genres
                        .AsNoTracking()
                        .ProjectTo<Genres.GenreDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return genres;
                }
            }
        }


        public class GetGenresByDescription
        {
            public class GetGenresByDescriptionQuery : IRequest<List<Genres.GenreDTO>>
            {
                public string Description { get; set; }
            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetGenresByDescriptionQuery, List<Genres.GenreDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<Genres.GenreDTO>> Handle(GetGenresByDescriptionQuery request, CancellationToken cancellationToken)
                {
                    var resp = await _context.Genres
                        .Where(g => g.Description.Contains(request.Description))
                        .ProjectTo<Genres.GenreDTO>(_mapper.ConfigurationProvider)
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

        public class CreateGenre
        {
            public class CreateGenreCommand: IRequest<Response>
            {
                public string Description { get; set; }
            }

            public class Validator: AbstractValidator<CreateGenreCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Description)
                        .NotEmpty().WithMessage("The description is required.")
                        .MaximumLength(100).WithMessage("The description must not exceed 100 characters.");
                }
            }

            public class Response 
            {                
                public Guid Id { get; set; }
                public string Description { get; set; }

                public static Response New(Guid id, string description)
                {
                    return new Response
                    {
                        Id = id,
                        Description = description
                    };
                }
            }

            public class CreateGenreCommandHandler(ApplicationDbContext context): IRequestHandler<CreateGenreCommand, Response>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<Response> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
                {
                    var newGenre = Genres.New(command.Description);
                    await _context.Genres.AddAsync(newGenre, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Response.New(newGenre.Id, newGenre.Description);
                }
            }
        }
    }
}
