namespace Magic.Application.Denominations.Commands
{
    public record UpdateDenominationCommand(DenominationDto Denomination, int Id)
        : ICommand<UpdateDenominationResponse>;

    public record UpdateDenominationResponse(int Id);

    public class UpdateDenominationHandler
    : ICommandHandler<UpdateDenominationCommand, UpdateDenominationResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public UpdateDenominationHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<UpdateDenominationResponse> Handle(UpdateDenominationCommand command, CancellationToken cancellationToken)
        {
            var existingDenomination = await _denominationSpecification.GetByIdAsync(d => d.Id == command.Id, cancellationToken);

            if (existingDenomination == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }
            existingDenomination.Update(
            command.Denomination.NameEN,
            command.Denomination.NameAR,
            command.Denomination.MaxValue,
            command.Denomination.MinValue,
            command.Denomination.IsInquiryRequired,
            command.Denomination.SortOrder,
            command.Denomination.ServiceId,
            command.Denomination.PriceType,
            command.Denomination.ProviderId,
            command.Denomination.IsActive
        );
            existingDenomination.UpdateAmounts(command.Denomination.Amounts?.Select(a => a.Value).ToList());
            existingDenomination.UpdateInputParameters(command.Denomination.InputParamterList.ToDenominationInputParameters());

            var denomination = DenominationExtensions.DtoToDenomination(command.Denomination);
            await _denominationSpecification.UpdateAsync(existingDenomination, cancellationToken);

            return new UpdateDenominationResponse(existingDenomination.Id);
        }
    }
}
