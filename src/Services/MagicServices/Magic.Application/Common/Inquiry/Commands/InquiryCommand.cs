namespace Magic.Application.Common.Inquiry.Commands
{
    public record InquiryCommand(InquiryRequestDto Request) : IRequest<InquiryResponseDto>, ITransactionRequest
    {
        public int DenominationId => Request.DenominationId;
    }

    public class InquiryCommandHandler : IRequestHandler<InquiryCommand, InquiryResponseDto>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly IExternalProviderInquiryService _externalProviderInquiryService;
        public InquiryCommandHandler(IExternalProviderInquiryService externalProviderInquiryService, IDenominationSpecification denominationSpecification)
        {
            _externalProviderInquiryService = externalProviderInquiryService;
            _denominationSpecification = denominationSpecification;
        }

        public async Task<InquiryResponseDto> Handle(InquiryCommand request, CancellationToken cancellationToken)
        {
            var denominationProvider = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(request.DenominationId, cancellationToken);

            if (denominationProvider.IsNullResult)
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);

            var inquiryRequestModel = request.Request.ToStandardRequest(denominationProvider.ProviderCode, denominationProvider.ProviderId);
            
            var response = await _externalProviderInquiryService.InquiryAsync(inquiryRequestModel, cancellationToken);
            return response.ToModelResponse();
        }
    }
}