using Magic.Application.Common.Interfaces;
using Magic.Domain.Specifications;

namespace Magic.Application.Common.Inquiry.Commands
{
    public record InquiryCommand(InquiryRequestDto Request) : IRequest<InquiryResponseDto>, ITransactionRequest
    {
        public int DenominationId => Request.DenominationId;
        public string ExternalId => Request.ExternalId;
    }

    public class InquiryCommandHandler : IRequestHandler<InquiryCommand, InquiryResponseDto>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly ILookUpSpecification _lookUpSpecification;
        private readonly IExternalApiProviderFactory _providerFactory;
        private readonly ILanguageService _languageService;
        public InquiryCommandHandler(IExternalApiProviderFactory providerFactory, IDenominationSpecification denominationSpecification, ILookUpSpecification lookUpSpecification, ILanguageService languageService)
        {
            _providerFactory = providerFactory;
            _denominationSpecification = denominationSpecification;
            _lookUpSpecification = lookUpSpecification;
            _languageService = languageService;
        }

        public async Task<InquiryResponseDto> Handle(InquiryCommand request, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(request.DenominationId, cancellationToken);

            if (denomination is null)
                throw new InquiryResponseException((int)DomainEnums.InternalErrorCode.EntityNotFound, _lookUpSpecification, _languageService.GetLanguage());

            // Step 1: Get the appropriate provider implementation based on the flag
            var provider = _providerFactory.GetProvider((DomainEnums.Provider)denomination.ProviderId);
            var response = await provider.SendInquiryRequestAsync(request.Request);
            return response;
        }
    }
}