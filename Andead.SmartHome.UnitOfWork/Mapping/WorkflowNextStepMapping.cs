using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class WorkflowNextStepMapping : IEntityTypeConfiguration<WorkflowNextStep>
    {
        public void Configure(EntityTypeBuilder<WorkflowNextStep> builder)
        {
            builder.ToTable("WorkflowNextStep");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.PreviousStepId).HasColumnName("PreviousStepId");
            builder.Property(x => x.NextStepId).HasColumnName("NextStepId");
        }
    }
}
