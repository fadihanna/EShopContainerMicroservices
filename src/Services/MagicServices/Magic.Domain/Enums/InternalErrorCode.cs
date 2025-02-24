namespace Magic.Domain.Enums
{
    public enum InternalErrorCode
    {
        Fail = 0,
        Success = 1,
        GeneralError = 2,
        IdentityUserAlreadyExist = 3,
        EntityNotFound = 4,
        DuplicateMobileNumber = 5,
        DuplicateEmail = 6,
        DuplicateEntry = 7,
        UserNotExist = 8,
        WrongPassword = 9,
        TokenNotVerified = 10,
        InvalidRefreshToken = 11,
        TokenNotFound = 12,
        TokenExpired = 13,//This token is expired. Please log in again
        TokenUsed = 14,
        TokenRevoked = 15,
        Status401Unauthorized = 16,
        Status403Forbidden = 17,
        UnAuthenticated = 18,
        InvalidTokenNoClaims = 19,//This is not our issued token. It has no claims.
        InvalidTokenNoUserId = 20,//This is not our issued token. It has no user ID
        InvalidTokenFormat = 21,//Invalid token format
        InvalidTokenNotExist = 22,//This token is not in our database
        TokenValidationFailed = 23//Token validation failed:
    }
}
