using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class WorkflowLogicMapping : IEntityTypeConfiguration<WorkflowLogic>
    {
        public void Configure(EntityTypeBuilder<WorkflowLogic> builder)
        {
            builder.ToTable("WorkflowLogic");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.LogicName).HasColumnName("LogicName");
            builder.Property(x => x.ClassName).HasColumnName("ClassName");
        }
    }
}
