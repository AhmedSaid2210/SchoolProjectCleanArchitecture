

using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    public class ClaimCommandHandler:ResponseHandler,
                                     IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        public ClaimCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IAuthorizationService authorizationService) :base(stringLocalizer) 
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var response =await _authorizationService.UpdateUserClaims(request);
            switch (response)
            {
                case "UserNotFound":
                    return NotFound<string>("User Not Found");

                case "FaildToRemoveClaims":
                    return BadRequest<string>("Faild To Remove Claims");

                case "FaildToAddNewClaims":
                    return BadRequest<string>("Faild To Add New Claims");

                default:
                    return Success("Success To Add New Claims");
            }

        }
    }
}
