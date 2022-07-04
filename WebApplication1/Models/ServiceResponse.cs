namespace WebApplication1.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public Boolean Success { get; set; } = true;

        public String message { get; set; } = null;
    }
}
