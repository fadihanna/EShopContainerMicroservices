using Magic.Application.Denominations.Queries.Denominations;

namespace MagicServices.API.Endpoints
{
    public class ServicesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/get-denominations", async ([AsParameters] int denominationId, ISender sender) =>
            {
                var result = await sender.Send(new GetDenominationByIdQuery(denominationId));

                var response = result.Adapt<GetDenominationByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetDenominations")
            .Produces<GetDenominationByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Denominations")
            .WithDescription("Get Denominations");
        }
    }
}
