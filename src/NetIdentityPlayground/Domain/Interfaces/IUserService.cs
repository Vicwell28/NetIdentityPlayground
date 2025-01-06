using NetIdentityPlayground.Application.Features.Users.Commands;

namespace NetIdentityPlayground.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(string username, string email, string password, string? phoneNumber);
    }
}
