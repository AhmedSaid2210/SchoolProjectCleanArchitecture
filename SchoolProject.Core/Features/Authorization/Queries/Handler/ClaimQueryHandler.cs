

using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class ClaimQueryHandler:ResponseHandler,
                                   IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        public ClaimQueryHandler(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService) :base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var response = await _authorizationService.GetUserClaimsAsync(request.UserId);
            if (response == null)
            {
                return NotFound<ManageUserClaimsResponse>("User not found");
            }
            return Success(response);
        }
    }
}
