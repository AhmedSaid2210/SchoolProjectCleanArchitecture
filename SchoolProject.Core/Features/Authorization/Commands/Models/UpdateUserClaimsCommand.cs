

using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand: UpdateUserClaimsResponse, IRequest<Response<string>>
    {
    }
}
