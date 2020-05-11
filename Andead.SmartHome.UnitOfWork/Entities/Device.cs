using Andead.SmartHome.UnitOfWork.Enums;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class Device : Entity
    {
        public string DeviceName { get; set; }

        public string IeeeAddress { get; set; }

        public string FriendlyName { get; set; }

        public string Type { get; set; }

        public long NetworkAddress { get; set; }

        public long ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public string PowerSource { get; set; }

        public string ModelId { get; set; }

        public virtual DeviceModel Model { get; set; }

        public DeviceStatus Status { get; set; }

        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
