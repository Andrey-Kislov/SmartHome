using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class ZigbeeEventMapping : IEntityTypeConfiguration<ZigbeeEvent>
    {
        public void Configure(EntityTypeBuilder<ZigbeeEvent> builder)
        {
            builder.ToTable("ZigbeeEvent");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.DateTime).HasColumnName("DateTime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.DeviceId).HasColumnName("DeviceId");
            builder.Property(x => x.DeviceAttributeId).HasColumnName("DeviceAttributeId");
            builder.Property(x => x.DeviceAttributeValue).HasColumnName("DeviceAttributeValue");

            builder.HasOne(x => x.Device).WithMany().HasForeignKey(x => x.DeviceId);
            builder.HasOne(x => x.DeviceAttribute).WithMany().HasForeignKey(x => x.DeviceAttributeId);
        }
    }
}
