
using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Denominations.Commands
{
    public record DeleteDenominationGroupCommand(int Id)
        : ICommand<DeleteDenominationGroupResponse>;

    public record DeleteDenominationGroupResponse(int Id);

    public class DeleteDenominationGroupHandler
    : ICommandHandler<DeleteDenominationGroupCommand, DeleteDenominationGroupResponse>
    {
        private readonly IDenominationGroupSpecification _denominationGroupSpecification;
        public DeleteDenominationGroupHandler(IDenominationGroupSpecification denominationGroupSpecification)
        {
            _denominationGroupSpecification = denominationGroupSpecification;
        }
        public async Task<DeleteDenominationGroupResponse> Handle(DeleteDenominationGroupCommand command, CancellationToken cancellationToken)
        {
            var existingDenomination = await _denominationGroupSpecification.GetByIdAsync(d => d.Id == command.Id, cancellationToken);

            if (existingDenomination == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            await _denominationGroupSpecification.DeleteAsync(existingDenomination, cancellationToken);

            return new DeleteDenominationGroupResponse(existingDenomination.Id);
        }
    }
}
