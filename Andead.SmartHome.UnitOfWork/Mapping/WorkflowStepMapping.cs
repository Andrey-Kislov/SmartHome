using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class WorkflowStepMapping : IEntityTypeConfiguration<WorkflowStep>
    {
        public void Configure(EntityTypeBuilder<WorkflowStep> builder)
        {
            builder.ToTable("WorkflowStep");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.WorkflowId).HasColumnName("WorkflowId");
            builder.Property(x => x.StepName).HasColumnName("StepName");
            builder.Property(x => x.WorkflowLogicId).HasColumnName("WorkflowLogicId");
            builder.Property(x => x.IsFirstStep).HasColumnName("IsFirstStep");

            builder.HasOne(x => x.Workflow).WithMany(x => x.Steps).HasForeignKey(x => x.WorkflowId);
            builder.HasOne(x => x.WorkflowLogic).WithMany(x => x.Steps).HasForeignKey(x => x.WorkflowLogicId);
        }
    }
}
