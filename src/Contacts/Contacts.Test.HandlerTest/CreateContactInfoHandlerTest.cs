using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.CQRS.Handlers.Command;
using Moq;
using Xunit;

namespace Contacts.Test.HandlerTest
{
    public class CreateContactInfoHandlerTest
    {
        [Fact]
        public async void CreateContactInfo_IsSuccess()
        {
            //arrange
            //act
            //assert

            ContactInfo contactInfo = new ContactInfo()
            {
                ContactId = 1,
                InfoTypeId = 1,
                Info = "test"
            };

            var mockUnitOfWorkService = new Mock<IContactInfoRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<ContactInfo>())).ReturnsAsync(contactInfo);

            var request = new CreateContactInfoDto()
            {
                ContactId = contactInfo.ContactId,
                InfoTypeId = contactInfo.InfoTypeId,
                Info = contactInfo.Info
            };

            var command = new CreateContactInfoHandler(mockUnitOfWorkService.Object);

            var response = await command.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async void CreateContactInfo_Failure()
        {
            //arrange
            //act
            //assert

            ContactInfo contactInfo = null;

            var mockUnitOfWorkService = new Mock<IContactInfoRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<ContactInfo>())).ReturnsAsync(contactInfo);

            CreateContactInfoDto request = null;

            var command = new CreateContactInfoHandler(mockUnitOfWorkService.Object);

            //null kontrolü olmasaydı test bu olacaktı.
            //var result = await Assert.ThrowsAsync<ArgumentNullException>(async () => 
            //    await command.Handle(request, CancellationToken.None));

            var response = await command.Handle(request, CancellationToken.None);

            Assert.Equal(response.Data.UUID, -1);
        }
    }
}