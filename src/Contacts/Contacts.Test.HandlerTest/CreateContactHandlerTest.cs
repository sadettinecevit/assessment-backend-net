using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.CQRS.Handlers.Command;
using Moq;
using Xunit;

namespace Contacts.Test.HandlerTest
{
    public class CreateContactHandlerTest
    {
        [Fact]
        public async void CreateContact_IsSuccess()
        {
            //arrange
            //act
            //assert

            Contact contact = new Contact()
            {
                Name = "Sadettin",
                Lastname = "Ecevit",
                Company = "Altınay"
            };

            var mockUnitOfWorkService = new Mock<IContactRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<Contact>())).ReturnsAsync(contact);

            var request = new CreateContactDto()
            {
                Name = contact.Name,
                Lastname = contact.Lastname,
                Company = contact.Company
            };

            var command = new CreateContactHandler(mockUnitOfWorkService.Object);

            var response = await command.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async void CreateContact_Failure()
        {
            //arrange
            //act
            //assert

            Contact contact = null;

            var mockUnitOfWorkService = new Mock<IContactRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<Contact>())).ReturnsAsync(contact);

            CreateContactDto request = null; // new CreateContactDto()
            //{
            //    Name = contact.Name,
            //    Lastname = contact.Lastname,
            //    Company = contact.Company
            //};

            var command = new CreateContactHandler(mockUnitOfWorkService.Object);

            var response = await command.Handle(request, CancellationToken.None);

            Assert.Equal(response.Data.UUID, -1);
        }
    }
}