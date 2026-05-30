

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Core.Features.Instructor.Queries.Models;
using SchoolProject.Core.Features.Instructor.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using static SchoolProject.Core.Features.Instructor.Queries.Response.GetAllInstructorResponse;

namespace SchoolProject.Core.Features.Instructor.Queries.Handlers
{
    public class InstructorHandler : ResponseHandler,
                                     IRequestHandler<GetAllInstructorQuery, Response<List<GetAllInstructorResponse>>>,
                                     IRequestHandler<GetInstructorByIdQuery,Response<GetInstructorByIdResponse>> ,
                                     IRequestHandler<GetInstructorPaginatedListQuery,PaginatedRasult<GetInstructorPaginatedListResponse>> 

    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        public InstructorHandler(IInstructorService instructorService,IMapper mapper,IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetAllInstructorResponse>>> Handle(GetAllInstructorQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _instructorService.GetAllInstructors();

            var instructorMapping = _mapper.Map<List<GetAllInstructorResponse>>(instructors);

            return Success(instructorMapping);
        }

        public async Task<Response<GetInstructorByIdResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorById(request.Id);

            var instructorMapping = _mapper.Map<GetInstructorByIdResponse>(instructor);
           

            return Success(instructorMapping);
        }

        public async Task<PaginatedRasult<GetInstructorPaginatedListResponse>> Handle(GetInstructorPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var instructor = _instructorService.GetInstructorsByFilterQueryable(request.OrderBy, request.Search);
            var instructorPaginated = await instructor.Select(i => new GetInstructorPaginatedListResponse
            {
                Id = i.Id,
                Address = i.Address,
                Name = i.Name,
                Position = i.Position,
                Salary = i.Salary,
                Department = new DepartmentIdAndName(i.Department.Id != null ? i.Department.Id : -1, i.Department.DName != null ? i.Department.DName : "Null"),
                DepartmentManger = new DepartmentIdAndName(i.DepartmentManger.Id != null ? i.DepartmentManger.Id : -1  , i.DepartmentManger.DName != null ? i.DepartmentManger.DName : "Null"),
                InstructorSupervisor = new InstructorIdAndName(i.InstructorSupervisor.Id != null ? i.InstructorSupervisor.Id: -1 , i.InstructorSupervisor.Name != null ? i.InstructorSupervisor.Name : "Null"),
                InstructorsSupervised = i.Instructors.Where(s => s.IsDeleted == false).Select(x => new InstructorIdAndName(x.Id, x.Name)).ToList()

            }).ToPaginatedListAsync(request.PageNumber, request.PageSize)
                ;
            return instructorPaginated;
        }
    }
}
