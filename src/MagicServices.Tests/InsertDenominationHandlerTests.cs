using Magic.Application.Denominations.Commands;
using Magic.Application.Dtos;
using Magic.Application.Exceptions;
using Magic.Application.Extensions;
using Magic.Domain.Enums;
using Magic.Domain.Models;
using Magic.Domain.Specifications;
using Moq;
namespace MagicServices.Tests
{
    public class InsertDenominationHandlerTests
    {
        private readonly Mock<IDenominationSpecification> _mockDenominationSpecification;
        private readonly InsertDenominationHandler _handler;

        public InsertDenominationHandlerTests()
        {
            _mockDenominationSpecification = new Mock<IDenominationSpecification>();
            _handler = new InsertDenominationHandler(_mockDenominationSpecification.Object);
        }

        //[Fact]
        //public async Task Handle_ShouldInsertDenomination_When_ValidRequest()
        //{
        //    var denominationDto = new DenominationDto(
        //        NameEN: "Test Denomination",
        //        NameAR: "اختبار الفئة",
        //        MaxValue: 500,
        //        MinValue: 10,
        //        IsInquiryRequired: true,
        //        SortOrder: 1,
        //        ServiceId: 123,
        //        PriceType: 2,
        //        ProviderId: 456,
        //        IsActive: true,
        //        DenominationGroupID: null,
        //        Value: 25,
        //        IsPartial: false,
        //        InputParamterList: new List<DenominationInputParameterList>
        //        {
        //            new DenominationInputParameterList("key1", "value1", 1, 10, "NameEn1", "NameAr1", "Code1", 1, true , "BillingAccount" ,"String"),
        //            new DenominationInputParameterList("key2", "value2", 2, 20, "NameEn2", "NameAr2", "Code2", 2, false, "Phone number ", "String")
        //        });

        //    var request = new InsertDenominationCommand(denominationDto);

        //    var denomination = denominationDto.DtoToDenomination();

        //        _mockDenominationSpecification
        // .Setup(x => x.InsertAsync(It.IsAny<Denomination>(), It.IsAny<CancellationToken>()))
        // .ReturnsAsync((Denomination d, CancellationToken _) => {
        //     d.GetType().GetProperty("Id")?.SetValue(d, 1); 
        //     return d;
        // });

        //    var result = await _handler.Handle(request, CancellationToken.None);

        //    Assert.NotNull(result);
        //    Assert.Equal(1, result.Id);

        //    _mockDenominationSpecification.Verify(x => x.InsertAsync(It.IsAny<Denomination>(), It.IsAny<CancellationToken>()), Times.Once);

        //    // Verify that amounts were properly added
         
        //    // Verify that input parameters were properly added
        //    Assert.Equal(denominationDto.InputParamterList.Count, denomination.DenominationInputParameters.Count);
        //    Assert.All(denomination.DenominationInputParameters, ip =>
        //        Assert.Contains(ip.Key, denominationDto.InputParamterList.Select(dto => dto.Key)));
        //}

        //[Fact]
        //public async Task Handle_ShouldThrowException_When_InsertFails()
        //{
        //    var denominationDto = new DenominationDto(
        //        NameEN: "Orange Card 10",
        //        NameAR: "كارت اورنج 10",
        //        MaxValue: 500,
        //        MinValue: 10,
        //        IsInquiryRequired: true,
        //        SortOrder: 1,
        //        ServiceId: 123,
        //        PriceType: 2,
        //        ProviderId: 456,
        //        IsActive: true,
        //        DenominationGroupID: 2,
        //        Value: 10,
        //        IsPartial: false,
        //        InputParamterList: new List<DenominationInputParameterList>());

        //    var request = new InsertDenominationCommand(denominationDto);

        //    _mockDenominationSpecification
        //        .Setup(x => x.InsertAsync(It.IsAny<Denomination>(), It.IsAny<CancellationToken>()))
        //        .ThrowsAsync(new InquiryResponseException(InternalErrorCode.Fail));

        //    var exception = await Assert.ThrowsAsync<InquiryResponseException>(() =>
        //        _handler.Handle(request, CancellationToken.None));

        //    Assert.Equal(InternalErrorCode.Fail, exception.ErrorCode);
        //}
    }
}
