using Magic.Domain.Enums;

namespace Magic.Application.Common.Interfaces
{
    public interface IInternalErrorCodeMapper
    {
        Task<string> GetErrorMessageAsync(InternalErrorCode errorCode, string language, CancellationToken cancellationToken);
    }
}
