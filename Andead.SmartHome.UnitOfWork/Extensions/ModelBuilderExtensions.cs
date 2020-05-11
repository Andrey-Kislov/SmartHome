using Andead.SmartHome.UnitOfWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "ivanov@test.ru",
                FirstName = "Ivan",
                LastName = "Ivanov"
            });

            modelBuilder.Entity<DeviceModel>().HasData(new DeviceModel
            {
                Id = 1,
                ModelName = "Aqara single key wireless wall switch (WXKG03LM)",
                ModelId = @"lumi.sensor_86sw1\u0000lu",
                ImageUrl = "/images/lumi.sensor_86sw1.png"
            });

            modelBuilder.Entity<DeviceAttribute>().HasData(new DeviceAttribute
            {
                Id = 1,
                AttributeFriendlyName = "Single click",
                AttributeName = "click",
                DeviceModelId = 1,
                AttributeType = Enums.AttributeType.Action
            });

            modelBuilder.Entity<Device>().HasData(new Device
            {
                Id = 1,
                DeviceName = "Switch on kitchen",
                UserId = 1,
                IeeeAddress = "0x00158d00010b4bd2",
                FriendlyName = "0x00158d00010b4bd2",
                Type = "EndDevice",
                NetworkAddress = 57055,
                ManufacturerId = 4151,
                ManufacturerName = "LUMI",
                PowerSource = "Battery",
                ModelId = @"lumi.sensor_86sw1\u0000lu",
                Status = Enums.DeviceStatus.Online
            });
        }
    }
}
