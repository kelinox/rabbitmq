namespace Microservices.Services.Core.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public static Response<T> Succeeded(T data)
        {
            return new Response<T>
            {
                Success = true,
                Data = data
            };
        }

        public static Response<T> Failure(string errorMessage)
        {
            return new Response<T> 
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }
}