using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<List<Role>> GetAllRolesAsync();
        public Task<Role> GetRoleByIdAsync(int id);
        public Task<string> AddRoleAsync(string roleName);
        public Task<string> EditRoleAsync(int id, string roleName);
        public Task<string> DeleteRoleAsync(int id);

        public Task<bool> IsRoleExistsAsync(string roleName);
        public Task<ManageUserRolesResponse> GetUserRolesAsync(int userId);

        public Task<string> UpdateUserRoles(UpdateUserRolesResponse manage);
        public Task<ManageUserClaimsResponse> GetUserClaimsAsync(int userId);
        public Task<string> UpdateUserClaims(UpdateUserClaimsResponse manage);

    }
}
