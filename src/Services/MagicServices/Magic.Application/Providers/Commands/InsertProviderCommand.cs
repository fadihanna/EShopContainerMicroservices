using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Providers.Commands
{
    public record InsertProviderCommand(string NameEN, string NameAR, bool IsActive, int SortOrder = 0)
        : ICommand<InsertProviderResponse>;

    public record InsertProviderResponse(int Id);

    public class InsertProviderHandler
        : ICommandHandler<InsertProviderCommand, InsertProviderResponse>
    {
        private readonly IProviderSpecification _providerSpecification;

        public InsertProviderHandler(IProviderSpecification providerSpecification)
        {
            _providerSpecification = providerSpecification;
        }

        public async Task<InsertProviderResponse> Handle(InsertProviderCommand command, CancellationToken cancellationToken)
        {
            var newProvider = Provider.Create(
                nameEn: command.NameEN,
                nameAr: command.NameAR,
                isActive: command.IsActive,
                sortOrder: command.SortOrder
            );

            await _providerSpecification.AddAsync(newProvider, cancellationToken);

            return new InsertProviderResponse(newProvider.Id);
        }
    }
}
