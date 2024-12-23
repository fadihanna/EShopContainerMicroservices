namespace Magic.Application.Denominations.Queries.Denominations
{
    public record GetDenominationByIdQuery(int Id)
    : IQuery<GetDenominationByIdResponse>;
    public record GetDenominationByIdResponse(DenominationDto denominationDto);
    public class GetDenominationByIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetDenominationByIdQuery, GetDenominationByIdResponse>
    {
        public async Task<GetDenominationByIdResponse> Handle(GetDenominationByIdQuery query, CancellationToken cancellationToken)
        {
            var denomination = await dbContext.Denominations
                    .AsNoTracking()
                    .Where(o => o.Id.Equals(query.Id))
                    .FirstOrDefaultAsync(cancellationToken);

            return new GetDenominationByIdResponse(denomination!.ToDenominationDto());
        }
    }
}