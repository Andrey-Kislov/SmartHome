using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class DeviceMapping : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(x => x.DeviceName).HasColumnName("DeviceName");
            builder.Property(x => x.IeeeAddress).HasColumnName("IeeeAddress");
            builder.Property(x => x.FriendlyName).HasColumnName("FriendlyName");
            builder.Property(x => x.Type).HasColumnName("Type");
            builder.Property(x => x.NetworkAddress).HasColumnName("NetworkAddress");
            builder.Property(x => x.ManufacturerId).HasColumnName("ManufacturerId");
            builder.Property(x => x.ManufacturerName).HasColumnName("ManufacturerName");
            builder.Property(x => x.PowerSource).HasColumnName("PowerSource");
            builder.Property(x => x.ModelId).HasColumnName("ModelId");
            builder.Property(x => x.Status).HasColumnName("Status");

            builder.HasIndex(x => x.IeeeAddress).IsUnique();

            builder.HasOne(x => x.Model).WithMany().HasForeignKey(x => x.ModelId).HasPrincipalKey(x => x.ModelId);
            builder.HasOne(x => x.User).WithMany(x => x.Devices).HasForeignKey(x => x.UserId);
        }
    }
}
