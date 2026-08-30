
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Responses;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        //private readonly ConcurrentDictionary<string,RefreshToken> _UserRefTokens;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            //_UserRefTokens = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (token, tokenHandler) =await GenerateJwtToken(user);

            var UserRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = GetRefreshToken(user.UserName).TokenString,
                ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                AddedTime = DateTime.Now,
                IsUsed = true,
                IsExpired = false,
                JwtId = token.Id,
                Token = tokenHandler,
            };
            var CreatedRefreshToken = await _refreshTokenRepository.AddAsync(UserRefreshToken);
           

            var response = new JwtAuthResult
            {
                AccessToken = tokenHandler,
                refreshToken = GetRefreshToken(user.UserName)
            };

            return response;
        }

        private async Task<(JwtSecurityToken ,string)> GenerateJwtToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var Claims = GetClaims(user, roles);

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                                             _jwtSettings.Audience,
                                             Claims,
                                             expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                                             signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256)
                );
            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            return (token, tokenHandler);
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };

            //_UserRefTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

            return refreshToken;
        }
        private List<Claim> GetClaims(User user, IEnumerable<string> roles)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.Id),          user.Id.ToString()),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                
            };
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return Claims;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if(string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException("accessToken is null");

            }
            var tokenHandler = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            return  tokenHandler;

        }
        public async Task<JwtAuthResult> GetRefreshToken(User user,string refreshToken, DateTime ExpireDate)
        {
           
            var (generateToken,Token) = await GenerateJwtToken(user);


            var response = new JwtAuthResult();

            response.AccessToken = Token;

            var RefreshTokenResult = new RefreshToken();
            RefreshTokenResult.UserName = generateToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            RefreshTokenResult.TokenString = refreshToken;
            RefreshTokenResult.ExpireAt = ExpireDate;

            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == user.Id && x.RefreshToken == refreshToken);

             userRefreshToken.Token = Token;

            await _refreshTokenRepository.UpdateAsync(userRefreshToken);

            response.refreshToken = RefreshTokenResult;

            return response;

        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validateToken = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = tokenHandler.ValidateToken(accessToken, validateToken, out SecurityToken ValidatedToken);
           
                if (validator == null) throw new SecurityTokenException("Invalid Token");

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return ("AlgorithmIsWrong",null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired",null);
            }
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Token == accessToken
                                                                                            && x.RefreshToken == refreshToken
                                                                                            && x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound",null);
            }

            if (userRefreshToken.ExpireDate < DateTime.UtcNow)
            {
                userRefreshToken.IsExpired = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);

                return ("RefreshTokenIsExpired",null);
            }
            return (userId,userRefreshToken.ExpireDate);
        }
    }
}
