using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Features.Subject.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    public class SubjectController : AppControllerBase
    {
        [HttpPost(Router.SubjectRouting.AddSubject)]
        public async Task<IActionResult> AddSubject(AddSubjectCommand command)
        {
            return NewResult( await _mediator.Send(command) );
        }
    }
}
