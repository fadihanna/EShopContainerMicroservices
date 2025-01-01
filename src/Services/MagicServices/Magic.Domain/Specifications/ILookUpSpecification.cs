using Magic.Domain.Enums;

namespace Magic.Domain.Specifications
{
    public interface ILookUpSpecification
    {
        Task<List<InternalErrorCodeLookup>> GetInternalErrorCodeLookupAsync(CancellationToken cancellationToken);
        string? GetErrorMessageAsync(int errorCode, string language, CancellationToken cancellationToken);
    }
}
