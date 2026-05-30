using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Student.Queries.Models;
using SchoolProject.Core.Features.Student.Queries.Response;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Student.Queries.Handlers
{
    public class StudentHandler : ResponseHandler,
                                  IRequestHandler<GetStudentsListQuery,Response<List<GetStudentsListResponse>>>,
                                  IRequestHandler<GetStudentByIdQuery ,Response<GetStudentByIdResponse>>,
                                  IRequestHandler<GetStudentPaginatedListQuery, PaginatedRasult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion

        #region Constructor
        public StudentHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer):base(stringLocalizer)
        {
            _studentService=studentService;
            _mapper=mapper;
            _stringLocalizer=stringLocalizer;
        }
        #endregion

        #region Handles Functions 
        public async Task<Response<List<GetStudentsListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {

            var studentsList =  await _studentService.GetAllStudents();
            var StudentMap   = _mapper.Map<List<GetStudentsListResponse>>(studentsList);

            return Success(StudentMap);
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await  _studentService.GetStudentById(request.Id);
            if (student == null) 
            {
                return NotFound<GetStudentByIdResponse>(_stringLocalizer[SharedResourceKeys.NotFound]);
            }
            var StudentMap = _mapper.Map<GetStudentByIdResponse>(student);
            //var StudentMap =new GetStudentByIdResponse { Id = student.Id ,Name = student.Name,Address=student.Address ,DepartmentName = student.Department.DName };
           // return ResponseHandler.NotFound<GetStudentByIdResponse>();
            return Success(StudentMap);
        }

        public async Task<PaginatedRasult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Data.Entities.Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.Id,e.Name,e.Address,e.Department.DName );
            var queryable = _studentService.GetStudentsByFilterQueryable( request.OrderBy , request.Search );
            var studentList = await queryable.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return studentList;
        }
        #endregion
    }
}
