

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler 
{
    public class RoleQueryHandler : ResponseHandler,
                                    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
                                    IRequestHandler<GetAllRoleQuery, Response<List<GetAllRoleResponse>>>,
                                    IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
                                    


    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        public RoleQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound<GetRoleByIdResponse>("Role not found");
            }

            var response =  _mapper.Map<GetRoleByIdResponse>(role);

            return Success(response);
        }

        public async Task<Response<List<GetAllRoleResponse>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetAllRolesAsync();
            if (roles == null)
            {
                return NotFound<List<GetAllRoleResponse>>("Role not found");
            }
            var response = _mapper.Map<List<GetAllRoleResponse>>(roles);

            return Success(response);
        }

        public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _authorizationService.GetUserRolesAsync(request.Id);

            if (userRoles == null)
            {
                return NotFound<ManageUserRolesResponse>("User roles not found");
            }

            return Success(userRoles);
        }
    }
}

