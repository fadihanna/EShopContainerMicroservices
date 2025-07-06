namespace Magic.Application.Common.Interfaces
{
    public interface IExternalProviderFeesService
    {
        Task<FeesResponseModel> FeesAsync(FeesRequestModel request, CancellationToken cancellationToken);
    }
}
