using Magic.Application.Common.Inquiry.Commands;

public class InquiryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/inquiry", async (InquiryRequestDto request, ISender sender) =>
        {
            var command = new InquiryCommand(request);
            var result = await sender.Send(command);

            if (result == null)
                return Results.BadRequest("Error processing the request.");

            return Results.Ok(result);
        }).WithName("Inquiry")
        .Produces<InquiryResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}