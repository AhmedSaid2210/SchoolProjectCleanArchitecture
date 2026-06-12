

using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IAuthenticationService _authenticationService;
        IStringLocalizer<SharedResource> _stringLocalizer;
        public AuthenticationQueryHandler(IAuthenticationService authenticationService,IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authenticationService = authenticationService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
            {
                return Success(result);
            }
            else return Unauthorized<string>("Expired");
    }
}
}
