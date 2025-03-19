
namespace Magic.Application.Queries
{
    public class GetServiceQuery : IQuery<GetServiceResponse>;
    public record GetServiceResponse(List<ServiceDto> serviceListDto);
    public class GetServiceHandler
    : IQueryHandler<GetServiceQuery, GetServiceResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;
        public GetServiceHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }
        public async Task<GetServiceResponse> Handle(GetServiceQuery query, CancellationToken cancellationToken)
        {
            var serviceList = await _serviceSpecification.GetAllAsync(cancellationToken);
            return new GetServiceResponse(serviceList!.ToServiceDtoList().ToList());
        }
    }
}
