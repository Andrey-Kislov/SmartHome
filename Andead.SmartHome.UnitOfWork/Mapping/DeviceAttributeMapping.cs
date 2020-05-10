using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class DeviceAttributeMapping : IEntityTypeConfiguration<DeviceAttribute>
    {
        public void Configure(EntityTypeBuilder<DeviceAttribute> builder)
        {
            builder.ToTable("DeviceAttribute");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(x => x.AttributeFriendlyName).HasColumnName("AttributeFriendlyName");
            builder.Property(x => x.AttributeName).HasColumnName("AttributeName");
            builder.Property(x => x.DeviceModelId).HasColumnName("DeviceModelId");

            builder.HasIndex(x => new { x.AttributeName, x.DeviceModelId }).IsUnique();
            builder.HasOne(x => x.DeviceModel).WithMany(x => x.Attributes).HasForeignKey(x => x.DeviceModelId);
        }
    }
}
