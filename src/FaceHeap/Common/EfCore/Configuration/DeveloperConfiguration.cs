using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceHeap.Common.EfCore.Configuration;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder.HasKey(x => x.Id).IsClustered(false);
        builder.HasAlternateKey(x => x.Name).IsClustered(true);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Popularity).HasDefaultValue(0);

        // My highly scalable application requires an index on the Name column
        // (this will have a negative with only 2 developers 😁, if the query plan chooses to use the index)
        builder.HasIndex(x => x.Name);
    }
}
