using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            var role = new Role();
            role.Name = roleName;
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return "Success";
            }
            return "Failed";
        }

        public async Task<string> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return "RoleNotFound";
            }
            var roleUsed = await _userManager.GetUsersInRoleAsync(role.Name);

            if (roleUsed.Any())
            {
                return "RoleInUse";
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return "Success";
            }

            return "Failed";
        }

        public async Task<string> EditRoleAsync(int id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return "RoleNotFound";
            }

            if (await IsRoleExistsAsync(roleName))
            {
                return "RoleAlreadyExists";
            }
            
            role.Name = roleName;
            var result =await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return "Success";
            }
            return "Failed";
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles.ToListAsync();

            if (roles == null)
            {
                return null;
            }
            return roles;
        }

        public Task<Role> GetRoleByIdAsync(int id)
        {
            var role = _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<bool> IsRoleExistsAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                return true;
            }
            return false;
        }
    }
}
