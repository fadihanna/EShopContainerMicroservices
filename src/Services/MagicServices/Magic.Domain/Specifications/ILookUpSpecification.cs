using Magic.Domain.Enums;

namespace Magic.Domain.Specifications;

public interface ILookUpSpecification
{
    Task<List<InternalErrorCodeLookup>> GetInternalErrorCodeLookupAsync();
    string GetErrorMessage(InternalErrorCode errorCode);
}
