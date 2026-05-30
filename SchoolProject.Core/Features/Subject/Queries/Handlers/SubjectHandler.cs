

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subject.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Subject.Queries.Handlers
{
    public class SubjectHandler : ResponseHandler,
                                  IRequestHandler<AddSubjectCommand,Response<string>>
                                  
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public SubjectHandler(ISubjectService subjectService, IMapper mapper,IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var Subject = _mapper.Map<Data.Entities.Subjects>(request);

            var subjectStatus = await _subjectService.AddAsync(Subject);

            if (subjectStatus == "Success") return Created<string>("Add Subject Successfully"); 

            else return BadRequest<string>(subjectStatus);
        }
    }
}
