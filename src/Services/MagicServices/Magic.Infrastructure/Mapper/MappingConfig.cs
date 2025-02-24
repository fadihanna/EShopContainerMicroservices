using BuildingBlocks.Models;
using Magic.Application.Dtos.Identity;
using Magic.Infrastructure.Data.Identity.Entity;
using Mapster;

namespace Magic.Infrastructure.Mapper
{
    internal class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ConsumerUser, ConsumerUserDto>().Map(dest => dest.Mobile, src => src.PhoneNumber);
            config.NewConfig<RegisterUserDto, ConsumerUser>().Map(dest => dest.PhoneNumber, src => src.Mobile);
            config.NewConfig<ConsumerUserDto, ConsumerUser>().Map(dest => dest.PhoneNumber, src => src.Mobile);
            config.NewConfig<RegisterUserDto, ConsumerUserDto>().Map(dest => dest.UserName, src => src.Mobile);
            config.NewConfig<RefreshToken, RefreshTokenDto>();
        }
    }
}
