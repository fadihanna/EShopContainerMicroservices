using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.DenominationGroups.Commands
{
    public record InsertDenominationGroupCommand(DenominationGroupDto DenominationGroup)
        : ICommand<InsertDenominationGroupResponse>;

    public record InsertDenominationGroupResponse(int Id);

    public class InsertDenominationGroupHandler
        : ICommandHandler<InsertDenominationGroupCommand, InsertDenominationGroupResponse>
    {
        private readonly IDenominationGroupSpecification _denominationGroupSpecification;

        public InsertDenominationGroupHandler(IDenominationGroupSpecification denominationGroupSpecification)
        {
            _denominationGroupSpecification = denominationGroupSpecification;
        }

        public async Task<InsertDenominationGroupResponse> Handle(
            InsertDenominationGroupCommand command,
            CancellationToken cancellationToken)
        {
            var denominationGroup = new DenominationGroup
            {
                NameEN = command.DenominationGroup.NameEN,
                NameAR = command.DenominationGroup.NameAR,
                IsActive = command.DenominationGroup.IsActive,
                SortOrder = command.DenominationGroup.SortOrder,
                IsInquiryRequired = command.DenominationGroup.IsInquiryRequired,
                ServiceId = command.DenominationGroup.ServiceId
            };

            await _denominationGroupSpecification.AddAsync(denominationGroup, cancellationToken);

            return new InsertDenominationGroupResponse(denominationGroup.Id);
        }
    }
}
