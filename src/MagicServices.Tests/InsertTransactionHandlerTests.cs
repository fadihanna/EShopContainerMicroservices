using BuildingBlocks.Exceptions;
using BuildingBlocks.Models;
using Magic.Application.Common.Payment.Commands;
using Magic.Domain.Models;
using Magic.Domain.Specifications;
using Moq;
using Payment.Service;

namespace MagicServices.Tests.Common.Payment.Commands
{
    //Below Test Cases are Unit Tests for handling transaction insertion in Database
    // using mocks to simulate the flow/logic (DB is not called)
    public class InsertTransactionHandlerTests
    {
        private readonly Mock<ITransactionSpecification> _mockTransactionSpecification;
        private readonly Mock<IPaymentGatewayService> _mockPaymentGatewayService;
        private readonly Mock<IDenominationSpecification> _mockDenominationSpecification;
        private readonly Mock<IRequestSepecification> _mockRequestSpecification;
        private readonly InsertTransactionHandler _handler;

        public InsertTransactionHandlerTests()
        {
            _mockTransactionSpecification = new Mock<ITransactionSpecification>();
            _mockPaymentGatewayService = new Mock<IPaymentGatewayService>();
            _mockDenominationSpecification = new Mock<IDenominationSpecification>();
            _mockRequestSpecification = new Mock<IRequestSepecification>();

            _handler = new InsertTransactionHandler(
                _mockRequestSpecification.Object,
                _mockDenominationSpecification.Object,
                _mockTransactionSpecification.Object,
                _mockPaymentGatewayService.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenDenominationIsNotFound()
        {
            var command = new InsertTransactionCommand(new PaymentRequestModel(

                Amount: 10,
                Fees: 5,
                TotalAmount: 100,
                DenominationId: 3,
                BillingAccount: "12345",
                quantity: 0,
                PaymentProviderId: 1,
                RefrenceTransactionId: "392039123213"
            ));

            _mockDenominationSpecification
                .Setup(x => x.GetByIdAsync(command.Transaction.DenominationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Denomination)null); // added null in return to simulate exception

            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnPaymentFailed_WhenStatusIsNotSuccess()
        {
            var command = new InsertTransactionCommand(new PaymentRequestModel(

                Amount: 10,
                Fees: 5,
                TotalAmount: 100,
                DenominationId: 3,
                BillingAccount: "12345",
                quantity: 0,
                PaymentProviderId: 1,
                RefrenceTransactionId: "392039123213"
            ));
            var paymentRequest = new PaymentGateway.Grpc.PaymentRequest
            {
                Amount = Convert.ToDouble(command.Transaction.TotalAmount),
                Provider = command.Transaction.PaymentProviderId.ToString(),
                Currency = "EGP"
            };

            var mockDenomination = new Denomination { Id = 3 }; // Mocked Denomination

            _mockDenominationSpecification
               .Setup(x => x.GetByIdAsync(command.Transaction.DenominationId, It.IsAny<CancellationToken>()))
               .ReturnsAsync(mockDenomination); // added null in return to simulate exception

            _mockPaymentGatewayService.Setup(x => x.ProcessPayment(paymentRequest, null))
                .ReturnsAsync(new PaymentGateway.Grpc.PaymentResponse() { Success = false });

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

        }
        [Fact]
        public async Task Handle_ShouldThrowException_WhenPaymentFails()
        {
            var command = new InsertTransactionCommand(new PaymentRequestModel(
                Amount: 10,
                Fees: 5,
                TotalAmount: 100,
                DenominationId: 3,
                BillingAccount: "12345",
                quantity: 0,
                PaymentProviderId: 1,
                RefrenceTransactionId: "392039123213"
            ));

            var paymentRequest = new PaymentGateway.Grpc.PaymentRequest
            {
                Amount = Convert.ToDouble(command.Transaction.TotalAmount),
                Provider = Convert.ToString(command.Transaction.PaymentProviderId), // Ensure Provider is an int if needed
                Currency = "EGP"
            };

            var mockDenomination = new Denomination { Id = 3 }; // Mocked Denomination

            _mockDenominationSpecification
       .Setup(x => x.GetByIdAsync(command.Transaction.DenominationId, It.IsAny<CancellationToken>()))
       .ReturnsAsync(mockDenomination); // Mocked response for denomination lookup with id of 3

            _mockPaymentGatewayService.Setup(x => x.ProcessPayment(It.IsAny<PaymentGateway.Grpc.PaymentRequest>(), null))
                .ReturnsAsync(new PaymentGateway.Grpc.PaymentResponse() { Success = false });

            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Payment failed", exception.Message); 
        }


        //success tests

        [Fact]
        public async Task Handle_ShouldCallPaymentProcessor_WhenDenominationIsValid()
        {
            var command = new InsertTransactionCommand(new PaymentRequestModel
            (
                DenominationId: 3,
                TotalAmount: 100,
                BillingAccount: "12345",
                PaymentProviderId: 1,
                quantity: 0,
                Amount: 90,
                Fees: 5,
                RefrenceTransactionId: "20252323"
            ));

            var mockDenomination = new Denomination { Id = command.Transaction.DenominationId };
            var mockPaymentResponse = new PaymentGateway.Grpc.PaymentResponse { Success = true };

            _mockDenominationSpecification
                .Setup(x => x.GetByIdAsync(command.Transaction.DenominationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockDenomination);

            _mockPaymentGatewayService
                .Setup(x => x.ProcessPayment(It.IsAny<PaymentGateway.Grpc.PaymentRequest>(), null))
                .ReturnsAsync(mockPaymentResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockPaymentGatewayService.Verify(x => x.ProcessPayment(It.IsAny<PaymentGateway.Grpc.PaymentRequest>(), null));
        }

        [Fact]
        public async Task Handle_ShouldInsertTransaction_WhenPaymentIsSuccessful()
        {
            var command = new InsertTransactionCommand(new PaymentRequestModel
            (
                DenominationId: 3,
                TotalAmount: 100,
                BillingAccount: "12345",
                PaymentProviderId: 1,
                quantity: 0,
                Amount: 90,
                Fees: 5,
                RefrenceTransactionId: "20252323"
            ));

            var mockDenomination = new Denomination { Id = command.Transaction.DenominationId };
            var mockPaymentResponse = new PaymentGateway.Grpc.PaymentResponse { Success = true };

            _mockDenominationSpecification
                .Setup(x => x.GetByIdAsync(command.Transaction.DenominationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockDenomination);

            _mockPaymentGatewayService
                .Setup(x => x.ProcessPayment(It.IsAny<PaymentGateway.Grpc.PaymentRequest>(), null))
                .ReturnsAsync(mockPaymentResponse);

            _mockTransactionSpecification
                .Setup(x => x.InsertAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None); // real execution

            // Assert
            _mockTransactionSpecification.Verify(x => x.InsertAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}