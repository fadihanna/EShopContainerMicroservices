using Magic.Domain.Enums;

namespace Magic.Application.Exceptions;
public class ForbiddenAccessException : Exception
{
    public InternalErrorCode ErrorCode { get; }

    public ForbiddenAccessException(InternalErrorCode errorCode)
    {
        ErrorCode = errorCode;
    }
}
