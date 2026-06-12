using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Queries.Models;
using SchoolProject.Core.Features.User.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
                               IRequestHandler<GetUserPaginatedQuery, PaginatedRasult<GetUserPaginatedResponse>>,
                               IRequestHandler<GetUserByIdQuery,Response<GetUserByIdResponse>>
            
    {
        private readonly UserManager<Data.Entities.Identity.User> _userManager;
        private readonly IMapper _mapper;
        public UserQueryHandler(UserManager<Data.Entities.Identity.User> userManager, IMapper mapper,
                            IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginatedRasult<GetUserPaginatedResponse>> Handle(GetUserPaginatedQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.AsQueryable();

            var result = await _mapper.ProjectTo<GetUserPaginatedResponse>(user)
                                .ToPaginatedListAsync(request.PageNumber, request.PageSize) ;

            return result ;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null) return NotFound<GetUserByIdResponse>("Not found");

            var userMapping = _mapper.Map<GetUserByIdResponse>(user);

            return Success(userMapping) ;
        }
    }
}
