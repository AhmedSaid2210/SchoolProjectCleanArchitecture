
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesResponse, IRequest<Response<string>>
    {
    } 
}
