using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Services;

namespace SchoolProject.Core.Features.Department.Commands.Handlers
{
    public class DepartmentHandler : ResponseHandler,
                                    IRequestHandler<AddDepartmentCommand, Response<string>>,
                                    IRequestHandler<UpdateDepartmentCommand, Response<string>>,
                                    IRequestHandler<DeleteDepartmentCommand, Response<string>>
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

        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Data.Entities.Department>(request);


            var departmentStatus = await _departmentService.AddAsync(department);

            if (departmentStatus == "Exsit") return UnprocessableEntity<string>("Name is Exsit");

            else if (departmentStatus == "Success") return Created("Add Successfully");

            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deleteStatus = await _departmentService.DeleteById(request.Id);

            if(deleteStatus == "Delete Success") return Deleted<string>(deleteStatus);
            else if(deleteStatus == "Error Delete Department Has Students") return UnprocessableEntity<string>(deleteStatus);
            else return BadRequest<string>(deleteStatus);
        }

        public async Task<Response<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Data.Entities.Department>(request);


            var departmentStatus = await _departmentService.UpdateAsync(department);

            if (departmentStatus == "Name Is Exsit") return UnprocessableEntity<string>("Name is Exsit");

            else if (departmentStatus == "Success") return Created("Update Successfully");

            else return BadRequest<string>(departmentStatus);
        }
    }
}
