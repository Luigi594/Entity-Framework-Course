using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static EFCoreCourse.Entities.Actors;

namespace EFCoreCourse.Server.Cruds
{
    public class ActorsCrudController
    {
        public class GetActors
        {
            public class  GetActorsQuery: IRequest<List<ActorsDTO>>
            {
                
            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetActorsQuery, List<ActorsDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<ActorsDTO>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
                {
                    var actors = await _context.Actors
                        .ProjectTo<ActorsDTO>(_mapper.ConfigurationProvider)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    return actors;
                }
            }
        }
    }
}
