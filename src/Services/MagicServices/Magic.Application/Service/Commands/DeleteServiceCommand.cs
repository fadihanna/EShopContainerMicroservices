using BuildingBlocks.Exceptions;

namespace Magic.Application.Commands
{
    public record DeleteServiceCommand(int Id)
         : ICommand<DeleteServiceResponse>;

    public record DeleteServiceResponse(int Id);

    public class DeleteServiceHandler : ICommandHandler<DeleteServiceCommand, DeleteServiceResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public DeleteServiceHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }
        public async Task<DeleteServiceResponse> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _serviceSpecification.GetByIdAsync(x => x.Id.Equals(command.Id), cancellationToken);
            if (service == null)
                throw new NotFoundException("Service", command.Id);

            await _serviceSpecification.DeleteAsync(service, cancellationToken);
            return new DeleteServiceResponse(service.Id);
        }
    }
}
