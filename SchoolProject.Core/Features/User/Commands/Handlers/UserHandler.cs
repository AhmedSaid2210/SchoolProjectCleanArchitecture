
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.User.Commands.Handlers
{
    public class UserHandler : ResponseHandler,
                               IRequestHandler<AddUserCommand,Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<Data.Entities.Identity.User> _userManager;
        public UserHandler(IStringLocalizer<SharedResource> stringLocalizer,
            IMapper mapper,
            UserManager<Data.Entities.Identity.User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) return BadRequest<string>("Email Is Exist");

            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userByUserName != null) return BadRequest<string>("User Name Is Exist");

            var userMapping = _mapper.Map<Data.Entities.Identity.User>(request);

            var identityUser = await _userManager.CreateAsync(userMapping, request.Password );

            if (!identityUser.Succeeded) return BadRequest<string>( identityUser.Errors.FirstOrDefault().Description);

            return Created<string>("Success");

        }
    }
}
