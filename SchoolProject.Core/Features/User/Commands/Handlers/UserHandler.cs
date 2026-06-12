
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.User.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.User.Commands.Handlers
{
    public class UserHandler : ResponseHandler,
                               IRequestHandler<AddUserCommand,Response<string>>,
                               IRequestHandler<UpdateUserCommand,Response<string>>,
                               IRequestHandler<DeleteUserCommand, Response<string>>,
                               IRequestHandler<ChangePasswordCommand,Response<string>>

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

            var users = await _userManager.Users.CountAsync();

            
            await _userManager.AddToRoleAsync(userMapping, "User");
            

            return Created<string>("Success");

        }
        

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();

            var userByUserName = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Id != user.Id);
            if (userByUserName != null) return BadRequest<string>("User Name Is Exist");

            //var userEmail = await _userManager.FindByEmailAsync(request.Email);

            //if (userByUserName != null && userEmail.Id != request.Id) return BadRequest<string>("Email Is Exist");

            var userMapping = _mapper.Map(request, user);

            var identityUser = await _userManager.UpdateAsync(userMapping);

            if (!identityUser.Succeeded) return BadRequest<string>(identityUser.Errors.FirstOrDefault().Description);

            return Success<string>("Success");
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();

            var identityUser = await _userManager.DeleteAsync(user);

            if (!identityUser.Succeeded) return BadRequest<string>(identityUser.Errors.FirstOrDefault().Description);

            return Created<string>("Delete Success");
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>();

            var checkPassword =  await _userManager.CheckPasswordAsync(user, request.CurrentPassword);

            if(checkPassword == false ) return BadRequest<string>("Current Password is Not qual");

            var changePassword = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if(!changePassword.Succeeded) return BadRequest<string>(changePassword.Errors.FirstOrDefault().Description);

            return Success<string>("Changed Successfully");

        }
    }
}
