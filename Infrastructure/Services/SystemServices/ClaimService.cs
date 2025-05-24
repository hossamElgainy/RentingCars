
using Core.DomainModels;
using Core.Interfaces.IServices.SystemIServices;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specifications;
using System.Security.Claims;

namespace Infrastructure.Services.SystemServices
{
    public class ClaimService(IUserService _userService, IUnitOfWork uow)
    {
        public async Task<ClaimsIdentity> CreateClaimsIdentityForUserAsync(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("image", user.Image ?? string.Empty),
                new Claim("Name", user.Name),
                new Claim("Role", string.Join(",", user.Role.ToString())),

            };



            return new ClaimsIdentity(claims);
        }

        

    }
}
