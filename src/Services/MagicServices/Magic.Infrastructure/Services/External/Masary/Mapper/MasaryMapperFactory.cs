using Magic.Infrastructure.Services.External.Masary.Models;

namespace Magic.Infrastructure.Services.External.Masary.Mapper
{
    public class MasaryMapperFactory : ExternalModelMapperBase<
    MasaryInquiryRequest,
    MasaryInquiryResponse,
    MasaryPaymentRequest,
    MasaryPaymentResponse>
    {
        public override MasaryInquiryRequest MapInquiryRequest(InquiryRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }

        public override InquiryResponseDto MapInquiryResponse(MasaryInquiryResponse providerResponse)
        {
            throw new NotImplementedException();
        }

        public override MasaryPaymentRequest MapPaymentRequest(PaymentRequestDto inquiryRequest)
        {
            throw new NotImplementedException();
        }

        public override PaymentResponseDto MapPaymentResponse(MasaryPaymentResponse providerResponse)
        {
            throw new NotImplementedException();
        }
    }
}
