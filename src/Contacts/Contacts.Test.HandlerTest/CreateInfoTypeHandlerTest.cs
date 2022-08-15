using Contacts.Application.Dto.Command;
using Contacts.Application.Interfaces.Repositories;
using Contacts.Domain.Entities;
using Contacts.Persistence.CQRS.Handlers.Command;
using Moq;
using Xunit;

namespace Contacts.Test.HandlerTest
{
    public class CreateInfoTypeHandlerTest
    {
        [Fact]
        public async void CreateInfoType_IsSuccess()
        {
            //arrange
            //act
            //assert

            InfoType infoType = new InfoType()
            {
                Name = "test"
            };

            var mockUnitOfWorkService = new Mock<IInfoTypeRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<InfoType>())).ReturnsAsync(infoType);

            CreateInfoTypeDto request = new CreateInfoTypeDto()
            {
                Name = infoType.Name
            };

            var command = new CreateInfoTypeHandler(mockUnitOfWorkService.Object);

            var response = await command.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async void CreateInfoType_Failure()
        {
            //arrange
            //act
            //assert

            InfoType contact = null;

            var mockUnitOfWorkService = new Mock<IInfoTypeRepository>();

            mockUnitOfWorkService.Setup(c => c.Add(It.IsAny<InfoType>())).ReturnsAsync(contact);

            CreateInfoTypeDto request = null;

            var command = new CreateInfoTypeHandler(mockUnitOfWorkService.Object);

            var response = await command.Handle(request, CancellationToken.None);

            Assert.Equal(response.Data.UUID, -1);
        }
    }
}