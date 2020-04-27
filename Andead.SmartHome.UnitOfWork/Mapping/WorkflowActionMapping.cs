using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class WorkflowActionMapping : IEntityTypeConfiguration<WorkflowAction>
    {
        public void Configure(EntityTypeBuilder<WorkflowAction> builder)
        {
            builder.ToTable("WorkflowAction");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.ActionName).HasColumnName("ActionName");
            builder.Property(x => x.WorkflowId).HasColumnName("WorkflowId");
            builder.Property(x => x.ClassName).HasColumnName("ClassName");
        }
    }
}
