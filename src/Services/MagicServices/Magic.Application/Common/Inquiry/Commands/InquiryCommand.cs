using Magic.Application.Common.Interfaces;
using Magic.Application.Denominations.Queries.Denominations;

namespace Magic.Application.Common.Inquiry.Commands
{
    public record InquiryCommand(InquiryRequestDto Request) : IRequest<InquiryResponseDto>, ITransactionRequest
    {
        public int DenominationId => Request.DenominationId;
        public string ExternalId => Request.ExternalId;
    }

    public class InquiryCommandHandler : IRequestHandler<InquiryCommand, InquiryResponseDto>
    {
        private readonly IExternalApiProviderFactory _providerFactory;
        private readonly IMediator _mediator;
        public InquiryCommandHandler(IExternalApiProviderFactory providerFactory, IMediator mediator)
        {
            _providerFactory = providerFactory;
            _mediator = mediator;
        }

        public async Task<InquiryResponseDto> Handle(InquiryCommand request, CancellationToken cancellationToken)
        {
            //Get DenominationById Details
            var denomination = await _mediator.Send(new GetDenominationByIdQuery(request.Request.DenominationId), cancellationToken);

            //check if denomination is active

            // Step 1: Get the appropriate provider implementation based on the flag
            var provider = _providerFactory.GetProvider((DomainEnums.Provider)denomination.denominationDto.ProviderId);
            var response = await provider.SendInquiryRequestAsync(request.Request);
            return response;
        }
    }
}