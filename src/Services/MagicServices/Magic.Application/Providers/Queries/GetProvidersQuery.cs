using Magic.Application.Interfaces.Specifications;

namespace Magic.Application.Providers.Queries
{
    public class GetProvidersQuery : IQuery<GetProvidersResponse>;

    public record GetProvidersResponse(List<ProviderDto> ProviderListDto);

    public class GetProvidersHandler
    : IQueryHandler<GetProvidersQuery, GetProvidersResponse>
    {
        private readonly IProviderSpecification _providerSpecification;

        public GetProvidersHandler(IProviderSpecification providerSpecification)
        {
            _providerSpecification = providerSpecification;
        }

        public async Task<GetProvidersResponse> Handle(GetProvidersQuery query, CancellationToken cancellationToken)
        {
            var providerList = await _providerSpecification.GetAllAsync(cancellationToken);
            return new GetProvidersResponse(providerList!.ToProviderDtoList().ToList());
        }
    }
}
