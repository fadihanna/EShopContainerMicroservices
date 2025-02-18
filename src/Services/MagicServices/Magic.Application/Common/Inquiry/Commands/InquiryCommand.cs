using Magic.Application.Common.Interfaces;
using Magic.Domain.Specifications;

namespace Magic.Application.Common.Inquiry.Commands
{
    public record InquiryCommand(InquiryRequestDto Request) : IRequest<InquiryResponseDto>, ITransactionRequest
    {
        public int DenominationId => Request.DenominationId;
    }

    public class InquiryCommandHandler : IRequestHandler<InquiryCommand, InquiryResponseDto>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        private readonly ILookUpSpecification _lookUpSpecification;
        private readonly IExternalProviderInquiryService _externalProviderInquiryService;
        private readonly ILanguageService _languageService;
        public InquiryCommandHandler(IExternalProviderInquiryService externalProviderInquiryService, IDenominationSpecification denominationSpecification, ILookUpSpecification lookUpSpecification, ILanguageService languageService)
        {
            _externalProviderInquiryService = externalProviderInquiryService;
            _denominationSpecification = denominationSpecification;
            _lookUpSpecification = lookUpSpecification;
            _languageService = languageService;
        }

        public async Task<InquiryResponseDto> Handle(InquiryCommand request, CancellationToken cancellationToken)
        {
            var denominationProvider = await _denominationSpecification.GetDenominationProviderCodeByIdAsync(request.DenominationId, cancellationToken);


            if (denominationProvider.IsNullResult)
                throw new InquiryResponseException((int)DomainEnums.InternalErrorCode.EntityNotFound, _lookUpSpecification, _languageService.GetLanguage());

            return null;

        }
    }
}