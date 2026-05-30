

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentHandler : ResponseHandler,
                                    IRequestHandler<GetAllDepartmentQuery, Response<List<GetAllDepartmentResponse>>>,
                                    IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
                                    IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedRasult<GetDepartmentPaginatedListResponse>>

    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public DepartmentHandler(IDepartmentService departmentService, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer):base(stringLocalizer)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<GetAllDepartmentResponse>>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllDepartments();

            var departmentMapping = _mapper.Map<List<GetAllDepartmentResponse>>(departments);

            return Success(departmentMapping);
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetDepartmentById(request.Id);

            if (departments == null) return UnprocessableEntity<GetDepartmentByIdResponse>("Not Found");

            var departmentMapping = _mapper.Map<GetDepartmentByIdResponse>(departments);

            return Success(departmentMapping);
        }

        public Task<PaginatedRasult<GetDepartmentPaginatedListResponse>> Handle(GetDepartmentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var query = _departmentService.GetDepartmentsByFilterQueryable(request.OrderBy, request.Search);
            var queryList = query
                .Select(d => new GetDepartmentPaginatedListResponse 
                { Id = d.Id,
                  DName = d.DName,
                  Students = d.Students.Where(s=>s.IsDeleted == false).Select(s => new StudentIdAndName { Id = s.Id, StudentsName = s.Name }).ToList(),
                  InstructorManager = new InstructorIdAndName { Id = d.InstructorManager.Id != null ? d.InstructorManager.Id : -1  , InstructorName = d.InstructorManager.Name != null ? d.InstructorManager.Name : "Null"},
                  Instructors = d.Instructors.Where(s => s.IsDeleted == false).Select(s => new InstructorIdAndName { Id = s.Id, InstructorName = s.Name }).ToList()
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return queryList;
        }
    }
}
