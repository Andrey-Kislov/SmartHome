using System;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class ZigbeeEvent : Entity
    {
        public DateTimeOffset DateTime { get; set; }

        public virtual Device Device { get; set; }

        public int DeviceId { get; set; }

        public virtual DeviceAttribute DeviceAttribute { get; set; }

        public int DeviceAttributeId { get; set; }

        public string DeviceAttributeValue { get; set; }
    }
}
