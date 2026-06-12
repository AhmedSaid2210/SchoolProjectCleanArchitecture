using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;

namespace SchoolProject.Infrustructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services) 
        {
            services.AddTransient<IStudentRepository,StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();



            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            
            return services;
        }

    }
}
