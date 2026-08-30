using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Responses;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace SchoolProject.Service.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext appDbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
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

        public async Task<ManageUserClaimsResponse> GetUserClaimsAsync(int userId)
        {
            var userById = await _userManager.FindByIdAsync(userId.ToString());

            if (userById == null)
            {
                return null;
            }
            var userClaims = await _userManager.GetClaimsAsync(userById);

            var response = new ManageUserClaimsResponse();

            response.UserId = userId;

            foreach (var claim in ClaimsStore.claims)
            {
                response.UserClaims.Add(new UserClaims
                {
                    Type  = claim.Type,
                    Value = (userClaims.FirstOrDefault(x => x.Type == claim.Type)?.Value ?? "false").ToLower() == "true" ,
                });

            }

            return response;

        }

        public async Task<ManageUserRolesResponse> GetUserRolesAsync(int userId)
        {
        
            var userById = await _userManager.FindByIdAsync(userId.ToString());

            if (userById == null)
            {
                return null;
            }

            var userRoles = new ManageUserRolesResponse
            {
                UserId = userById.Id,
                UserRoles = new List<UserRoles>()
            };

           var roles = await _userManager.GetRolesAsync(userById);

           var allRoles = await _roleManager.Roles.ToListAsync();

            foreach (var role in allRoles)
            {
                userRoles.UserRoles.Add(new UserRoles
                { 
                    Id = role.Id,
                    Name = role.Name,
                    HasRole = roles.Contains(role.Name)
                });
            }

            return userRoles ;
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

        public async Task<string> UpdateUserClaims(UpdateUserClaimsResponse request)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {

                var userById = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (userById == null)
                {
                    return "UserNotFound";
                }

                var getUserClaim = await _userManager.GetClaimsAsync(userById);
                var removeClaim = await _userManager.RemoveClaimsAsync(userById, getUserClaim);

                if (!removeClaim.Succeeded) return "FaildToRemoveClaims";

                var claimList = request.UserClaims.Where(x => x.Value == true).Select( x=> new Claim(x.Type, x.Value.ToString().ToLower()));

                var addNewClaim = await _userManager.AddClaimsAsync(userById, claimList);

                if (!addNewClaim.Succeeded) return "FaildToAddNewClaims";


                await transaction.CommitAsync();

                return "Success";

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return "FaildToAddNewClaims";
            }
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesResponse request)
        {
            var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {

                var userById = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (userById == null)
                {
                    return "UserNotFound";
                }

                var userRole = await _userManager.GetRolesAsync(userById);

                var deleteResult = await _userManager.RemoveFromRolesAsync(userById, userRole);

                if (!deleteResult.Succeeded)
                {
                    return "FaildToRemoveOldRoles";
                }

                var rolesName = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                var addNewRoles = await _userManager.AddToRolesAsync(userById, rolesName);

                if (!addNewRoles.Succeeded)
                {
                    return "FaildToAddNewRoles";
                }

                await transaction.CommitAsync();

                return "Success";

            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();

                return "FaildToAddNewRoles";
            }
        }
    }
}
