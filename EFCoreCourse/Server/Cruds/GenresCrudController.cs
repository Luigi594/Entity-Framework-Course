using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            public class CreateGenreCommand: IRequest<ActionResult<Response>>
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

            public class CreateGenreCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateGenreCommand, ActionResult<Response>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<Response>> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
                {
                    var genreExists = await _context.Genres
                        .Where(x => x.Description == command.Description)
                        .Select(x=>x.Description)
                        .ToListAsync(cancellationToken);

                    if (genreExists.Count > 0)
                    {
                        for (int i = 0; i < genreExists.Count; i++)
                        {
                            var genre = genreExists.ElementAt(i);
                            if (genre == command.Description)
                            {
                                throw new Exception($"The genre with the description '{command.Description}' already exists.");
                            }
                        }
                    }

                    var newGenre = Genres.New(command.Description);
                    await _context.Genres.AddAsync(newGenre, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return Response.New(newGenre.Id, newGenre.Description);
                }
            }
        }

        public class CreateGenresByList
        {
            public class CreateGenresByListCommand : IRequest<ActionResult<Response>>
            {
                public List<string> Description { get; set; }
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

            public class CreateGenreBylistCommandHandler(ApplicationDbContext context) 
                : IRequestHandler<CreateGenresByListCommand, ActionResult<Response>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<Response>> Handle(CreateGenresByListCommand command, CancellationToken cancellationToken)
                {
                    var existingGenres = await _context.Genres
                        .Where(g => command.Description.Contains(g.Description))
                        .Select(g => g.Description)
                        .ToListAsync(cancellationToken);

                    if(existingGenres.Count > 0)
                    {
                        var existingGenresList = string.Join(", ", existingGenres);
                        throw new Exception($"The following genres already exist: {existingGenresList}");
                    }

                    var newGenres = command.Description.Distinct().ToList();

                    foreach(var genre in newGenres)
                    {
                        var newGenre = Genres.New(genre);
                        await _context.Genres.AddAsync(newGenre, cancellationToken);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return Response.New("All genres created succesfully!");
                }
            }
        }
    }
}
