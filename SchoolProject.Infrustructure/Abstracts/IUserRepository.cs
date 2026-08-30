

using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IUserRepository:IGenericRepositoryAsync<User>
    {
    }
}
