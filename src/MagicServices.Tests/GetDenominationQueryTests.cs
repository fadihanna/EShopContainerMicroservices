using BuildingBlocks.Exceptions;
using Magic.Application.Denominations.Queries.Denominations;
using Magic.Domain.Models;
using Magic.Domain.Specifications;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Magic.Domain.Enums;
using Magic.Application.Exceptions;

namespace MagicServices.Tests
{
    public class GetDenominationQueryTests
    {
        private readonly Mock<IDenominationSpecification> _mockDenominationSpecification;
        private readonly GetDenominationByIdHandler _handler; 

        public GetDenominationQueryTests()
        {
            _mockDenominationSpecification = new Mock<IDenominationSpecification>();

            _handler = new GetDenominationByIdHandler(
               _mockDenominationSpecification.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_When_DenominationId_NotFound()
        {
            // Arrange
            var denominationId = 1000;
            var query = new GetDenominationByIdQuery(denominationId);

            _mockDenominationSpecification
                .Setup(x => x.GetByIdAsync(d=>d.Id == query.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Denomination)null);

            var exception = await Assert.ThrowsAsync<InquiryResponseException>(() =>
                _handler.Handle(query, CancellationToken.None));

            Assert.Equal(InternalErrorCode.EntityNotFound, exception.ErrorCode);
        }

    }
}
