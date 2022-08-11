using MediatR;

namespace Contacts.Application.Dto
{
    public class ContactDto : IRequest<ContactResponse>
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
    }
}
