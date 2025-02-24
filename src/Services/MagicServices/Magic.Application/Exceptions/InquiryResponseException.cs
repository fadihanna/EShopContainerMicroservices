using Magic.Domain.Enums;

namespace Magic.Application.Exceptions
{
    public class InquiryResponseException : Exception
    {
        public InternalErrorCode ErrorCode { get; }
        public InquiryResponseException(InternalErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
