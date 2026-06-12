
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string,DateTime?)> ValidateDetails(JwtSecurityToken jwtSecurity,string accessToken, string refreshToken);
        public Task<JwtAuthResult> GetRefreshToken(User user, string refreshToken, DateTime ExpireDate);
        public Task<string> ValidateToken(string accessToken);

    }
}
