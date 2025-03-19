using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Denominations.Queries.Denominations
{
    public record GetDenominationInputParamByDenominationIdQuery(int Id)
    : IQuery<GetDenominationInputParamByDenominationIdResponse>;
    public record GetDenominationInputParamByDenominationIdResponse(DenominationDto denominationDto);

    public class GetDenominationInputParamByDenominationIdHandler: IQueryHandler<GetDenominationInputParamByDenominationIdQuery, GetDenominationInputParamByDenominationIdResponse>
    {
        private readonly IDenominationSpecification _denominationSpecification;
        public GetDenominationInputParamByDenominationIdHandler(IDenominationSpecification denominationSpecification)
        {
            _denominationSpecification = denominationSpecification;
        }
        public async Task<GetDenominationInputParamByDenominationIdResponse> Handle(GetDenominationInputParamByDenominationIdQuery query, CancellationToken cancellationToken)
        {
            var denomination = await _denominationSpecification.GetByIdAsync(o => o.IsActive && o.Id.Equals(query.Id), cancellationToken);

            if (denomination == null)
                throw new InquiryResponseException(DomainEnums.InternalErrorCode.EntityNotFound);

            return new GetDenominationInputParamByDenominationIdResponse(denomination!.ToDenominationDto());
        }
    }
}
