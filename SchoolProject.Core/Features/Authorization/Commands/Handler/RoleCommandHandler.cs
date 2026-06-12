

using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    
    public class RoleCommandHandler : ResponseHandler,
                                      IRequestHandler<AddRoleCommand, Response<string>>,
                                      IRequestHandler<EditRoleCommand, Response<string>>,
                                      IRequestHandler<DeleteRoleCommand, Response<string>>

    {
        private readonly IAuthorizationService _authorizationService;
        public RoleCommandHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
         
           var result = await _authorizationService.AddRoleAsync(request.RoleName);
        

           if (result == "Failed")
           {
              return BadRequest<string>("Role Create Failed");
                
           }
           return Success("Role created successfully");
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
          var  status = await _authorizationService.EditRoleAsync(request.Id, request.RoleName);
            switch(status) 
            {
                case "RoleNotFound":
                    {
                        return NotFound<string>("Role not found");
                    }
                case "RoleAlreadyExists":
                    {
                        return BadRequest<string>("Role already exists");
                    }
                case "Failed":
                    {
                        return BadRequest<string>("Role update failed");
                    }
                default:
                    {
                        return Success("Role updated successfully");
                    }
            }
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var status = await _authorizationService.DeleteRoleAsync(request.Id);
            switch (status)
            {
                case "RoleNotFound":
                    {
                        return NotFound<string>("Role not found");
                    }
                case "RoleInUse":
                    {
                        return BadRequest<string>("Role is in use" );
                    }
                case "Failed":
                    {
                        return BadRequest<string>("Role Delete failed");
                    }
                default:
                    {
                        return Success("Role Delete successfully");
                    }
            }
        }
    }
}
