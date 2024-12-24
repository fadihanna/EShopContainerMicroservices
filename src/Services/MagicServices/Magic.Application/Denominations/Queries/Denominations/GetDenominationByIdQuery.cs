using Magic.Domain.Specifications;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public record GetDenominationByIdQuery(int Id)
    : IQuery<GetDenominationByIdResponse>;
    public record GetDenominationByIdResponse(DenominationDto denominationDto);
    public class GetDenominationByIdHandler
    : IQueryHandler<GetDenominationByIdQuery, GetDenominationByIdResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public GetDenominationByIdHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<GetDenominationByIdResponse> Handle(GetDenominationByIdQuery query, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(query.Id, cancellationToken);
            return new GetDenominationByIdResponse(denomination!.ToDenominationDto());
        }
    }
}