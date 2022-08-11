using MediatR;

namespace Contacts.Application.Dto
{
    public class ContactInfoDto : IRequest<ContactInfoResponse>
    {
        public int ContactId { get; set; }
        public int InfoTypeId { get; set; }
        public string Info { get; set; }
    }
}
