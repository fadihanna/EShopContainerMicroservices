using BuildingBlocks.Exceptions;

namespace Magic.Application.Exceptions;
public class DenominationNotFoundException : NotFoundException
{
    public DenominationNotFoundException(Int64 id) : base("Denomination", id)
    {
    }
}
