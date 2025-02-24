using Magic.Application.Dtos.Identity;
using Magic.Domain.Enums;
using Magic.Domain.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Magic.Infrastructure.Data.Specifications
{
    public abstract class BaseSpecification
    {
        private ILookUpSpecification _lookUpSpecification;
        public BaseSpecification(ILookUpSpecification lookUpSpecification) 
        {
            _lookUpSpecification = lookUpSpecification;
        }
        protected async Task<T> ExecuteWithHandling<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
            {
                return (T)(object)IdentityResponseDto.Failed(new IdentityError
                {
                    Code = ((int)InternalErrorCode.DuplicateEntry).ToString(),
                    Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.DuplicateEntry)
                });
            }
            catch (Exception ex)
            {
                return (T)(object)IdentityResponseDto.Failed(new IdentityError
                {
                    Code = ((int)InternalErrorCode.GeneralError).ToString(),
                    Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.GeneralError)
                });
            }
        }
    }

}
