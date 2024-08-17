using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
namespace FITrack.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;
        private readonly IMapper _mapper;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IOptions<Jwt> jwt
            , IMapper mapper )
        {
            _userManager = userManager;
            _roleManager = roleManager;   
            _jwt = jwt.Value;
            _mapper = mapper;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered !" };
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "UserName is already registered !" };
            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                StringBuilder Errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    Errors.Append($"{error.Description} , ");
                }
                return new AuthModel { Message = Errors.ToString() };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtTokenAsync(user);
            return new AuthModel
            {
                UserId = user.Id,
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user , model.Password))
            {
                authModel.Message = "Email Or Password Is Incorrect";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtTokenAsync(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.UserId = user.Id;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Roles = rolesList.ToList();
            return authModel;
        }
        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid email or role";
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to role";
            var result =  await _userManager.AddToRoleAsync(user, model.Role);
            return result.Succeeded ? string.Empty : "Something want wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles) 
                roleClaims.Add(new Claim("roles" , role));
            var Claims = new Claim[]
            {
                new Claim("uid" , user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                expires: DateTime.Now.AddMinutes(_jwt.Lifetime),
                signingCredentials: signingCredentials,
                claims: Claims);

            return jwtSecurityToken;
        }
    }
}
