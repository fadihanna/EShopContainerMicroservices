using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Providers.Commands
{
    public record DeleteProviderCommand(int Id)
        : ICommand<DeleteProviderResponse>;

    public record DeleteProviderResponse(int Id);

    public class DeleteProviderHandler
        : ICommandHandler<DeleteProviderCommand, DeleteProviderResponse>
    {
        private readonly IProviderSpecification _providerSpecification;

        public DeleteProviderHandler(IProviderSpecification providerSpecification)
        {
            _providerSpecification = providerSpecification;
        }

        public async Task<DeleteProviderResponse> Handle(DeleteProviderCommand command, CancellationToken cancellationToken)
        {
            var existingProvider = await _providerSpecification.GetByIdAsync(p => p.Id == command.Id, cancellationToken);

            if (existingProvider == null)
            {
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);
            }

            await _providerSpecification.DeleteAsync(existingProvider, cancellationToken);

            return new DeleteProviderResponse(existingProvider.Id);
        }
    }
}
