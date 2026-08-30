
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Enums;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        IQueryable<User> GetUsersQueryable();
        IQueryable<User> GetUsersByFilterQueryable(UserOrderingEnum orderingEnum, string? search = null);
        Task<User> GetUserById(int id);
    }
}
