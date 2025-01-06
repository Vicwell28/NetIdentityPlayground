namespace NetIdentityPlayground.Application.Common.DTOs
{
    public class ErrorDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string? Solution { get; set; }

        public ErrorDto(string code, string description, string? solution = null)
        {
            Code = code;
            Description = description;
            Solution = solution;
        }
    }
}
