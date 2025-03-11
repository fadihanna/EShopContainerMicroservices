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
            var denomination = DenominationExtensions.DtoToDenomination(command.Denomination);
            await _denominationSpecification.InsertAsync(denomination, cancellationToken);

            return new InsertDenominationResponse(denomination.Id);
        }
    }
}
