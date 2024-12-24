using Magic.Domain.Specifications;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public class GetDenominationsQuery: IQuery<GetDenominationsResponse>;
    public record GetDenominationsResponse(List<DenominationDto> denominationListDto);
    public class GetDenominationsHandler
    : IQueryHandler<GetDenominationsQuery, GetDenominationsResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public GetDenominationsHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<GetDenominationsResponse> Handle(GetDenominationsQuery query, CancellationToken cancellationToken)
        {
            var denominationList = await _denominationSpecification.GetAllAsync(cancellationToken);
            return new GetDenominationsResponse(denominationList!.ToDenominationDtoList().ToList());
        }
    }
}