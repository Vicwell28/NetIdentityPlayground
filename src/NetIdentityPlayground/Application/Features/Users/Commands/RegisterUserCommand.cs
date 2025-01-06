using MediatR;

namespace NetIdentityPlayground.Application.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Username { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
