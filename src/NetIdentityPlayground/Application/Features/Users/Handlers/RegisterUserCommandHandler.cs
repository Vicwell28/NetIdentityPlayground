using MediatR;
using NetIdentityPlayground.Application.Features.Users.Commands;
using NetIdentityPlayground.Domain.Interfaces;

namespace NetIdentityPlayground.Application.Features.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        public IUserService _userService { get; set; }
        
        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.RegisterUserAsync(request.Username, request.Email, request.Password, request.PhoneNumber);
        }
    }
}
