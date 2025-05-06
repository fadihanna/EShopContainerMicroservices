using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Providers.Commands
{
    public record UpdateProviderCommand(ProviderDto Provider, int Id)
        : ICommand<UpdateProviderResponse>;

    public record UpdateProviderResponse(int Id);

    public class UpdateProviderHandler
        : ICommandHandler<UpdateProviderCommand, UpdateProviderResponse>
    {
        private readonly IProviderSpecification _providerSpecification;

        public UpdateProviderHandler(IProviderSpecification providerSpecification)
        {
            _providerSpecification = providerSpecification;
        }

        public async Task<UpdateProviderResponse> Handle(UpdateProviderCommand command, CancellationToken cancellationToken)
        {
            var existingProvider = await _providerSpecification.GetByIdAsync(p => p.Id == command.Id, cancellationToken);

            if (existingProvider == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            existingProvider.Update(
                command.Provider.NameEN,
                command.Provider.NameAR,
                command.Provider.IsActive
                );

            await _providerSpecification.UpdateAsync(existingProvider, cancellationToken);

            return new UpdateProviderResponse(existingProvider.Id);
        }
    }
}
