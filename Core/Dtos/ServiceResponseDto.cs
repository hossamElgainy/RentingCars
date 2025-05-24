namespace Core.Dtos
{
    public class ServiceResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ServiceResponseDto(T data, string message = "", bool isSuccess = true)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }

        public ServiceResponseDto(string message, bool isSuccess = false)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}