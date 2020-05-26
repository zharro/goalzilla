using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using Goalzilla.Goalzilla.Application.Common.ViewModels;
using Goalzilla.Goalzilla.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Application.Users
{
    public class UserGetById
    {
        public class Query : IRequest<Maybe<UserViewModel>>
        {
            [FromRoute(Name = "userId")]
            public Guid UserId { get; set; }
        }
        
        public class Handler : IRequestHandler<Query, Maybe<UserViewModel>>
        {
            private readonly GoalzillaDbContext _dbContext;
            private readonly IMapper _mapper;
            
            public Handler(GoalzillaDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }
            
            public async Task<Maybe<UserViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _dbContext.Users
                    .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            }
        }
    }
}