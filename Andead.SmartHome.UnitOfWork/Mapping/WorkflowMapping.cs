using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class WorkflowMapping : IEntityTypeConfiguration<Workflow>
    {
        public void Configure(EntityTypeBuilder<Workflow> builder)
        {
            builder.ToTable("Workflow");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.WorkflowName).HasColumnName("WorkflowName");
            builder.Property(x => x.DeviceId).HasColumnName("DeviceId");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.IsActive).HasColumnName("IsActive");

            builder.HasMany(x => x.Steps).WithOne(x => x.Workflow);
        }
    }
}
