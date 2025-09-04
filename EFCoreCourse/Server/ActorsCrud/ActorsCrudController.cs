using EFCoreCourse.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse.Server.ActorsCrud
{
    public class ActorsCrudController
    {
        public class GetActors
        {
            public class  GetActorsQuery: IRequest<List<ActorsDTO>>
            {
                
            }
            public class Handler(ApplicationDbContext context) : IRequestHandler<GetActorsQuery, List<ActorsDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                public async Task<List<ActorsDTO>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
                {
                    var actors = await _context.Actors
                        .Select(x => new ActorsDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                            LastName = x.LastName,
                            BirthDate = x.BirthDate
                        })
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    return actors;
                }
            }
        }
    }
}
