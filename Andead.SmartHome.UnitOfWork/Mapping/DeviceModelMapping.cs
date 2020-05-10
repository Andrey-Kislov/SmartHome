using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Andead.SmartHome.UnitOfWork.Mapping
{
    public class DeviceModelMapping : IEntityTypeConfiguration<DeviceModel>
    {
        public void Configure(EntityTypeBuilder<DeviceModel> builder)
        {
            builder.ToTable("DeviceModel");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Property(x => x.ModelName).HasColumnName("ModelName");
            builder.Property(x => x.ModelId).HasColumnName("ModelId");
            builder.Property(x => x.ImageUrl).HasColumnName("ImageUrl");

            builder.HasIndex(x => x.ModelId).IsUnique();
        }
    }
}
