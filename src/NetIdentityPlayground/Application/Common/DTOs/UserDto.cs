namespace NetIdentityPlayground.Application.Common.DTOs
{
    public class UserDto
    {
        public UserDto() { }

        public int Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
