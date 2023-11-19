using Bristows.TRACR.Model.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bristows.TRACR.Model.Contexts.Configurations;

public sealed class PFUserConfigurations : IEntityTypeConfiguration<PeopleFinderUser>
{
    public void Configure(EntityTypeBuilder<PeopleFinderUser> builder)
    {
        builder.HasNoKey();
        builder.Property(p => p.PFID)  //telling EF not to generate values for PFID or treat it as the table key
            .ValueGeneratedNever();  
        builder.Property(p => p.FeeEarnerChargeOutRate)
            .HasColumnType("decimal(18,2)");
    }
}
