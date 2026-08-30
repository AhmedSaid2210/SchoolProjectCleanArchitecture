
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsersByFilterQueryable(UserOrderingEnum orderingEnum, string? search = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsersQueryable()
        {
            return _userRepository.GetTableNoTracking().AsQueryable();
        }
    }
}
