namespace Magic.Application.Queries
{
    public record GetServiceByIdQuery(int Id)
   : IQuery<GetServiceIdResponse>;
    public record GetServiceIdResponse(ServiceDto serviceDto);
    public class GetServiceByIdHandler
    : IQueryHandler<GetServiceByIdQuery, GetServiceIdResponse>
    {
        private readonly IServiceSpecification _serviceSpecification;
        public GetServiceByIdHandler(IServiceSpecification serviceSpecification)
        {
            _serviceSpecification = serviceSpecification;
        }
        public async Task<GetServiceIdResponse> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            var service = await _serviceSpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(query.Id), cancellationToken);

            if (service == null)
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);

            return new GetServiceIdResponse(service!.ToServiceDto());
        }
    }
}
