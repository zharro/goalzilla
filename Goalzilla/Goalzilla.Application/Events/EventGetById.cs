using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using Goalzilla.Goalzilla.Application.Events.ViewModels;
using Goalzilla.Goalzilla.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Application.Events
{
    public class EventGetById 
    {
        public class Query : IRequest<Maybe<EventViewModel>>
        {
            [FromRoute(Name = "eventId")]
            public Guid EventId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Maybe<EventViewModel>>
        {
            private readonly GoalzillaDbContext _dbContext;
            private readonly IMapper _mapper;
            
            public Handler(GoalzillaDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            
            public async Task<Maybe<EventViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _dbContext.Events
                    .ProjectTo<EventViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);
            }
        }
    }
}