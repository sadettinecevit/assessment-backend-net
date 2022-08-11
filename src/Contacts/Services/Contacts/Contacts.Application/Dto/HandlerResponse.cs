namespace Contacts.Application.Dto
{
    public class HandlerResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
