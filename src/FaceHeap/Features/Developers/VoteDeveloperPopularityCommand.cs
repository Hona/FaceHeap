using Microsoft.EntityFrameworkCore;

namespace FaceHeap.Features.Developers;

public sealed record VoteDeveloperPopularityRequest(string Name);

internal sealed class VoteDeveloperPopularityCommand(AppDbContext db)
    : Endpoint<VoteDeveloperPopularityRequest, NoContent>
{
    public override void Configure()
    {
        Post("/developers/{Name}/popularity");
        AllowAnonymous();
        Throttle(25, 3);
    }

    public override async Task HandleAsync(
        VoteDeveloperPopularityRequest request,
        CancellationToken cancellationToken
    )
    {
        await db
            .Developers.Where(x => x.Name == request.Name)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.Popularity, p => p.Popularity + 1),
                cancellationToken
            );

        await db
            .Developers.Where(x => x.Name != request.Name)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.Popularity, p => p.Popularity - 1),
                cancellationToken
            );

        await SendResultAsync(TypedResults.NoContent());
    }
}
