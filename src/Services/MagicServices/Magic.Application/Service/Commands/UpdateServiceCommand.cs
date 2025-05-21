using BuildingBlocks.Exceptions;

namespace Magic.Application.Commands
{
    public record UpdateServiceCommand(ServiceDto service,int Id)
        : ICommand<UpdateServiceResponse>;

    public record UpdateServiceResponse(int Id);

    public class UpdateServiceHandler : ICommandHandler<UpdateServiceCommand, UpdateServiceResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public UpdateServiceHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }
        public async Task<UpdateServiceResponse> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var existingService = await _serviceSpecification.GetByIdAsync(x => x.Id.Equals(command.Id), cancellationToken);
            if (existingService == null)
                throw new NotFoundException("Service", command.Id);

            existingService.NameEN = command.service.NameEN;
            existingService.NameAR = command.service.NameAR;
            existingService.SortOrder = command.service.SortOrder;
            existingService.IsActive = command.service.IsActive;
            existingService.ServiceCategoryId = command.service.ServiceCategoryId;

            await _serviceSpecification.UpdateAsync(existingService, cancellationToken);
            return new UpdateServiceResponse(existingService.Id);
        }
    }
}
