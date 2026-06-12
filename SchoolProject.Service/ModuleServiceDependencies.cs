using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Services;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService   , StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<ISubjectService   , SubjectService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>(); 
            services.AddTransient<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
