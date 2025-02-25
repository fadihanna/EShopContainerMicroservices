using BuildingBlocks.Exceptions;
using Magic.Application.Common.Payment.Commands;
using Magic.Application.Common.Payment.Queries;
using Magic.Domain.Abstractions;
using Magic.Domain.Models;
using Magic.Domain.Specifications;
using Microsoft.JSInterop;
using Moq;
//using Payment.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace MagicServices.Tests

{ //Below Test Cases are Unit Tests for handling transaction queries from Database
    // using mocks to simulate the flow/logic (DB is not called)
    public class GetTransactionQueryTests
    {
        private readonly Mock<ITransactionSpecification> _mockTransactionSpecification;
        private readonly Mock<IDenominationSpecification> _mockDenominationSpecification;
        private readonly Mock<IRequestSepecification> _mockRequestSpecification;
        private readonly GetTransactionByIdHandler _mockGetTransactionByIdHandler;
        private readonly GetTransactionByUserIdHandler _mockGetTransactionByUserIdHandler;
        public GetTransactionQueryTests()
        {
            _mockTransactionSpecification = new Mock<ITransactionSpecification>();
            _mockDenominationSpecification = new Mock<IDenominationSpecification>();
            _mockRequestSpecification = new Mock<IRequestSepecification>();

            _mockGetTransactionByIdHandler = new GetTransactionByIdHandler(
                _mockTransactionSpecification.Object);

            _mockGetTransactionByUserIdHandler = new GetTransactionByUserIdHandler(
                _mockTransactionSpecification.Object);
        }
        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_When_Transaction_InvoiceIdIsNotFound()
        {
            var getTransactionByIdQuery = new GetTransactionByIdQuery(
            Id: 1000
            );

            _mockTransactionSpecification
        .Setup(x => x.GetByInvoiceId(getTransactionByIdQuery.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Magic.Domain.Models.Transaction)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _mockGetTransactionByIdHandler.Handle(getTransactionByIdQuery, CancellationToken.None));
            Assert.Contains($"Entity \"Transaction\" ({getTransactionByIdQuery.Id})", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_When_Transaction_UserIdIsNotFound()
        {
            var getTransactionByUserIdQuery = new GetTransactionByUserIdQuery(
                   UserId : "george"
                );
            _mockTransactionSpecification.Setup(x => x.GetByUserId(getTransactionByUserIdQuery.UserId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Magic.Domain.Models.Transaction>)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _mockGetTransactionByUserIdHandler.Handle(getTransactionByUserIdQuery, CancellationToken.None));
            Assert.Contains("No Transactions Found for User",exception.Message); 

        }

        //success test cases 

        [Fact]
        public async Task Handle_ShouldReturnTransaction_When_Transaction_InvoiceIdExists()
        {
            var mockedTransaction = new Magic.Domain.Models.Transaction
            {
                Id = 1000,
                UserId = "george",
                Amount = 50,
                Fees = 5,
                TotalAmount = 55,
                BillingAccount = "12345",
                Status = 1
            };

            _mockTransactionSpecification
                .Setup(x => x.GetByInvoiceId(mockedTransaction.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockedTransaction);

            var query = new GetTransactionByIdQuery(mockedTransaction.Id);
            var result = await _mockGetTransactionByIdHandler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            //Assert.Equal(expectedTransaction.Id, result.transactionDto.TransactionId);
            Assert.Equal(mockedTransaction.UserId, result.transactionDto.UserId);
        }

    }
}
