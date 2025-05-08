namespace Magic.Application.Extensions;

public static class ProviderExtensions
{
    public static IEnumerable<ProviderDto> ToProviderDtoList(this IEnumerable<Provider> providers)
    {
        return providers.Select(ToProviderDto);
    }

    public static ProviderDto ToProviderDto(this Provider provider)
    {
        return new ProviderDto(
            Id: provider.Id,
            NameEN: provider.NameEN,
            NameAR: provider.NameAR,
            IsActive: provider.IsActive
           
        );
    }

    public static Provider DtoToProvider(this ProviderDto dto)
    {
        var provider = Provider.Create(
            dto.NameEN,
            dto.NameAR,
            dto.IsActive
        );

        return provider;
    }
}
