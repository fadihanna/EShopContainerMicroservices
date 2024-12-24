using Magic.Application.Common.Interfaces;
using Magic.Application.Denominations.Queries.Denominations;
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
        private readonly IExternalApiProviderFactory _providerFactory;
        public InquiryCommandHandler(IExternalApiProviderFactory providerFactory, IDenominationSpecification denominationSpecification)
        {
            _providerFactory = providerFactory;
            _denominationSpecification = denominationSpecification;
        }

        public async Task<InquiryResponseDto> Handle(InquiryCommand request, CancellationToken cancellationToken)
        {
            //Get DenominationById Details
            var denomination = await _denominationSpecification.GetByIdAsync(request.DenominationId, cancellationToken);

            //check if denomination is active

            // Step 1: Get the appropriate provider implementation based on the flag
            var provider = _providerFactory.GetProvider((DomainEnums.Provider)denomination.ProviderId);
            var response = await provider.SendInquiryRequestAsync(request.Request);
            return response;
        }
    }
}