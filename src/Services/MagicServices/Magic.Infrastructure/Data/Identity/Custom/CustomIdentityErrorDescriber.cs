using Magic.Domain.Enums;
using Magic.Domain.Specifications;
using Microsoft.AspNetCore.Identity;

namespace Magic.Infrastructure.Data.Identity.Custom
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        // Make these fields private & set them in each request scope
        private ILookUpSpecification _lookUpSpecification;
        public CustomIdentityErrorDescriber(ILookUpSpecification lookUpSpecification) 
        {
            _lookUpSpecification = lookUpSpecification;
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = ((int)InternalErrorCode.DuplicateMobileNumber).ToString(),
                Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.DuplicateMobileNumber)
            };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = ((int)InternalErrorCode.DuplicateEmail).ToString(),
                Description = _lookUpSpecification.GetErrorMessage(InternalErrorCode.DuplicateEmail)
            };
        }
    }
}
