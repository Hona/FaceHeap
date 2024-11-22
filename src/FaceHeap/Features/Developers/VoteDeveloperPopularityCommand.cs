using Microsoft.EntityFrameworkCore;

namespace FaceHeap.Features.Developers;

public sealed record VoteDeveloperPopularityRequest(string Name, PopularityVote Vote);

internal sealed class VoteDeveloperPopularityCommand(AppDbContext db)
    : Endpoint<VoteDeveloperPopularityRequest, NoContent>
{
    public override void Configure()
    {
        Post("/developers/{Name}/popularity");
        AllowAnonymous();
        Throttle(60, 10);
    }

    public override async Task HandleAsync(
        VoteDeveloperPopularityRequest request,
        CancellationToken cancellationToken
    )
    {
        var delta = request.Vote switch
        {
            PopularityVote.Increase => 1,
            PopularityVote.Decrease => -1,
            _ => throw new ArgumentOutOfRangeException(nameof(request.Vote)),
        };

        await db
            .Developers.Where(x => x.Name == request.Name)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.Popularity, p => p.Popularity + delta),
                cancellationToken
            );

        await SendResultAsync(TypedResults.NoContent());
    }
}
