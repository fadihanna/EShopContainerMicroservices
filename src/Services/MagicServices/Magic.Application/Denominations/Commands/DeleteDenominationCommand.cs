namespace Magic.Application.Denominations.Commands
{
    public record DeleteDenominationCommand(int Id)
        : ICommand<DeleteDenominationResponse>;

    public record DeleteDenominationResponse(int Id);

    public class DeleteDenominationHandler
    : ICommandHandler<DeleteDenominationCommand, DeleteDenominationResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public DeleteDenominationHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<DeleteDenominationResponse> Handle(DeleteDenominationCommand command, CancellationToken cancellationToken)
        {
            var existingDenomination = await _denominationSpecification.GetByIdAsync(d => d.Id == command.Id, cancellationToken);

            if (existingDenomination == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            await _denominationSpecification.DeleteAsync(existingDenomination, cancellationToken);

            return new DeleteDenominationResponse(existingDenomination.Id);
        }
    }
}
