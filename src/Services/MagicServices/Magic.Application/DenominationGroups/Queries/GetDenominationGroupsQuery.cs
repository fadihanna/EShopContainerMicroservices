using Magic.Application.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.DenominationGroups.Queries
{
    public class GetDenominationGroupsQuery : IQuery<GetDenominationGroupsResponse>;

    public record GetDenominationGroupsResponse(List<DenominationGroupDto> DenominationGroupListDto);

    public class GetDenominationGroupsHandler
        : IQueryHandler<GetDenominationGroupsQuery, GetDenominationGroupsResponse>
    {
        private readonly IDenominationGroupSpecification _specification;

        public GetDenominationGroupsHandler(IDenominationGroupSpecification specification)
        {
            _specification = specification;
        }

        public async Task<GetDenominationGroupsResponse> Handle(GetDenominationGroupsQuery query, CancellationToken cancellationToken)
        {
            var groups = await _specification.GetAllAsync(cancellationToken);
            return new GetDenominationGroupsResponse(groups!.ToDenominationGroupDtoList().ToList());
        }
    }
}
