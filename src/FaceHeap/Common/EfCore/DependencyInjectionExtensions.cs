using Microsoft.EntityFrameworkCore;

namespace FaceHeap.Common.EfCore;

public static class DependencyInjectionExtensions
{
    public static void AddAppDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
}
