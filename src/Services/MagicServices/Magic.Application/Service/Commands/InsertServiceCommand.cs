namespace Magic.Application.Commands
{
    public record InsertServiceCommand(ServiceDto service)
        : ICommand<InsertServiceResponse>;

    public record InsertServiceResponse(int Id);

    public class InsertServiceHandler : ICommandHandler<InsertServiceCommand, InsertServiceResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;

        public InsertServiceHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }
        public async Task<InsertServiceResponse> Handle(InsertServiceCommand command, CancellationToken cancellationToken)
        {
            var service = ServiceExtension.DtoToService(command.service);
            await _serviceSpecification.InsertAsync(service, cancellationToken);
            return new InsertServiceResponse(service.Id);
        }
    }
}
