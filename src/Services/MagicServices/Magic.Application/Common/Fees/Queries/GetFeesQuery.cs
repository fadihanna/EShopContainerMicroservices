namespace Magic.Application.Common.Fees.Queries
{
    public record GetFeesQuery(FeesRequestDto Request) : IQuery<GetFeesResponse>;
    public record GetFeesResponse(FeesResponseDto FeesResponseDto);
    public class GetFeesHandler
    : IQueryHandler<GetFeesQuery, GetFeesResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IExternalProviderFeesService _externalProviderFeesService;
        public GetFeesHandler(IDenominationSpecification denominationSpecification, IExternalProviderFeesService externalProviderFeesService)
        {
            _denominationSpecification = denominationSpecification;
            _externalProviderFeesService = externalProviderFeesService;
        }
        public async Task<GetFeesResponse> Handle(GetFeesQuery query, CancellationToken cancellationToken)
        {
            var denominationProvider = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(query.Request.DenominationId, cancellationToken);

            if (denominationProvider.IsNullResult)
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);

            var feesRequestModel = query.Request.ToStandardRequest(denominationProvider.ProviderCode);

            var response = await _externalProviderFeesService.FeesAsync(feesRequestModel, cancellationToken);

            return new GetFeesResponse(response.ToModelResponse());
        }
    }
}
