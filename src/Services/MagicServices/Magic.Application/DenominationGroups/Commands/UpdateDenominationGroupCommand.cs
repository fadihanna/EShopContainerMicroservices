using Magic.Application.Interfaces.Specifications;
using Magic.Application.Dtos;
using Magic.Domain.Exceptions;
using Magic.Domain.Enums;
using Magic.Domain.Models.Lookups;

namespace Magic.Application.DenominationGroups.Commands
{
    public record UpdateDenominationGroupCommand(DenominationGroupDto DenominationGroup, int Id)
        : ICommand<UpdateDenominationGroupResponse>;

    public record UpdateDenominationGroupResponse(int Id);

    public class UpdateDenominationGroupHandler
        : ICommandHandler<UpdateDenominationGroupCommand, UpdateDenominationGroupResponse>
    {
        private readonly IDenominationGroupSpecification _denominationGroupSpecification;

        public UpdateDenominationGroupHandler(IDenominationGroupSpecification denominationGroupSpecification)
        {
            _denominationGroupSpecification = denominationGroupSpecification;
        }

        public async Task<UpdateDenominationGroupResponse> Handle(UpdateDenominationGroupCommand command, CancellationToken cancellationToken)
        {
            var existingGroup = await _denominationGroupSpecification.GetByIdAsync(
                g => g.Id == command.Id,
                cancellationToken);

            if (existingGroup == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

             existingGroup.NameEN = command.DenominationGroup.NameEN;
            existingGroup.NameAR = command.DenominationGroup.NameAR;
            existingGroup.IsActive = command.DenominationGroup.IsActive;
            existingGroup.SortOrder = command.DenominationGroup.SortOrder;
            existingGroup.IsInquiryRequired = command.DenominationGroup.IsInquiryRequired;
            existingGroup.ServiceId = command.DenominationGroup.ServiceId;

            await _denominationGroupSpecification.UpdateAsync(existingGroup, cancellationToken);

            return new UpdateDenominationGroupResponse(existingGroup.Id);
        }
    }
}
