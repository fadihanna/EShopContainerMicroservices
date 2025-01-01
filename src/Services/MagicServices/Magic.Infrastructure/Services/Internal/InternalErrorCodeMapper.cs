using Magic.Domain.Enums;
using Magic.Domain.Specifications;

namespace Magic.Infrastructure.Services.Internal
{
    public class InternalErrorCodeMapper : IInternalErrorCodeMapper
    {
        private readonly ILookUpSpecification _lookUpSpecification;

        public InternalErrorCodeMapper(ILookUpSpecification lookUpSpecification)
        {
            _lookUpSpecification = lookUpSpecification;
        }

        public async Task<string> GetErrorMessageAsync(InternalErrorCode errorCode, string language, CancellationToken cancellationToken)
        {
            var message = _lookUpSpecification.GetErrorMessageAsync((int)errorCode, language, cancellationToken);
            return message ?? "An unknown error occurred.";
        }
    }
}
