namespace NetIdentityPlayground.Application.Common.DTOs
{
    public class ResponseDto<T> : BaseResponseDto
    {
        public T? Data { get; set; }

        public virtual void SetSuccess(T data, string? message = null)
        {
            base.SetSuccess(message, 200, "Exitoso");

            Data = data;
        }
    }
}