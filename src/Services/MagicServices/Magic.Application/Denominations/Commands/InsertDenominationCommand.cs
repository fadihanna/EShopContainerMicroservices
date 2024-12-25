using Magic.Application.Common.Interfaces;
using Magic.Domain.Specifications;

namespace Magic.Application.Denominations.Commands
{
    public record InsertDenominationCommand(DenominationDto Denomination)
        : ICommand<InsertDenominationResponse>;

    public record InsertDenominationResponse(int Id);

    public class InsertDenominationHandler
    : ICommandHandler<InsertDenominationCommand, InsertDenominationResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public InsertDenominationHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<InsertDenominationResponse> Handle(InsertDenominationCommand command, CancellationToken cancellationToken)
        {
            var denomination = CreateNewDenomination(command.Denomination);
            await _denominationSpecification.InsertDenominationAsync(denomination, cancellationToken);

            return new InsertDenominationResponse(denomination.Id);
        }

        private Denomination CreateNewDenomination(DenominationDto denominationDto)
        {

            var newDenomination = Denomination.Create(
                nameEn: denominationDto.NameEN,
                nameAr: denominationDto.NameAR,
                value: denominationDto.Value,
                minValue: denominationDto.MinValue,
                maxValue: denominationDto.MaxValue,
                isInquiryRequired: denominationDto.IsInquiryRequired,
                sortOrder: denominationDto.SortOrder,
                serviceId: denominationDto.ServiceId,
                priceType: denominationDto.PriceType,
                providerId: denominationDto.ProviderId,
                isActive: denominationDto.IsActive
            );

            return newDenomination;
        }
    }
}
