using Microsoft.AspNetCore.Identity;
using NetIdentityPlayground.Application.Features.Users.Commands;
using NetIdentityPlayground.Domain.Interfaces;

namespace NetIdentityPlayground.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password, string? phoneNumber)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var user = new IdentityUser();

            user.UserName = username;
            user.Email = email;

            if (phoneNumber != null)
            {
                user.PhoneNumber = phoneNumber;
            }

            var resultado = await _userManager.CreateAsync(user, password);

            if (!resultado.Succeeded)
            {
                throw new Exception("No se pudo crear el usuario por los siguientes errores: " + string.Join(",", resultado.Errors.Select(it => it.Description)));
            }

            return resultado.Succeeded;
        }
    }
}
