using Microsoft.EntityFrameworkCore;

namespace FaceHeap.Features.Developers;

public sealed record GetDeveloperPopularityRequest(string Name);

internal sealed class GetDeveloperPopularityQuery(AppDbContext db)
    : Endpoint<GetDeveloperPopularityRequest, Ok<int>>
{
    public override void Configure()
    {
        Get("/developers/{Name}/popularity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetDeveloperPopularityRequest popularityRequest,
        CancellationToken cancellationToken
    )
    {
        var response = await db
            .Developers.AsNoTracking()
            .Where(x => x.Name == popularityRequest.Name)
            .Select(x => x.Popularity)
            .SingleAsync(cancellationToken);

        await SendResultAsync(TypedResults.Ok(response));
    }
}
