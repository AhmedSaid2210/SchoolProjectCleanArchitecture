
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Abstracts;


namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
                                                IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
                                                IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>

    {
        private readonly IMapper _mapper;
        private readonly UserManager<Data.Entities.Identity.User> _userManager;
        private readonly SignInManager<Data.Entities.Identity.User> _signInManager ;
        private readonly IAuthenticationService _authenticationService ;
        public AuthenticationCommandHandler(UserManager<Data.Entities.Identity.User> userManager
            , SignInManager<Data.Entities.Identity.User> signInManager, IMapper mapper,
            IAuthenticationService authenticationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByNameAsync(request.UserName);

            if (User == null) return NotFound<JwtAuthResult>("UserName Not Found");

            var signInResult =  await _signInManager.CheckPasswordSignInAsync(User, request.Password,false);

            if (!signInResult.Succeeded) return NotFound<JwtAuthResult>("Password Not Correct");

            var token = await _authenticationService.GetJWTToken(User);

            return Success(token);

        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken =  _authenticationService.ReadJwtToken(request.AccessToken);
            var (userId , dateExpire) = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken,request.RefreshToken);
            switch(userId)
            {
                case "AlgorithmIsWrong": return Unauthorized<JwtAuthResult>("Algorithm Is Wrong"); 
                case "TokenIsNotExpired": return Unauthorized<JwtAuthResult>("Token Is Not Expired");
                case "RefreshTokenIsNotFound": return Unauthorized<JwtAuthResult>("Refresh Token Is Not Found");
                case "RefreshTokenIsExpired": return Unauthorized<JwtAuthResult>("Refresh Token Is Expired");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>("User Is Not Found");
            }

            var token = await _authenticationService.GetRefreshToken(user,request.RefreshToken, dateExpire ?? DateTime.Now);

            return Success(token);
        }
    }
}
