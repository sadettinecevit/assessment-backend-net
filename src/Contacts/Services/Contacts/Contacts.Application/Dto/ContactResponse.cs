namespace Contacts.Application.Dto
{
    public class ContactResponse
    {
        public bool IsSuccess { get; set; }
        public ContactDto ContactDto { get; set; }
    }
}
