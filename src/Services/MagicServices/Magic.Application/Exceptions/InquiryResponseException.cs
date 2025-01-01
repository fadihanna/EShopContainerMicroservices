using BuildingBlocks.Exceptions;
using Magic.Domain.Specifications;

namespace Magic.Application.Exceptions
{
    public class InquiryResponseException : NotFoundException
    {
        public int ErrorCode { get; }
        public string? ErrorMessage { get; }
        public InquiryResponseException(int errorCode, ILookUpSpecification lookUpSpecification, string language) : base(errorCode)
        {
            if (lookUpSpecification == null)
                throw new ArgumentNullException(nameof(lookUpSpecification));

            ErrorMessage = lookUpSpecification.GetErrorMessageAsync(errorCode, language, CancellationToken.None);
            ErrorCode = errorCode;
        }
        public override string Message => ErrorMessage ?? base.Message;
    }
}
