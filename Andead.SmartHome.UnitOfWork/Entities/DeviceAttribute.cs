using Andead.SmartHome.UnitOfWork.Enums;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class DeviceAttribute : Entity
    {
        public int DeviceId { get; set; }

        public string AttributeName { get; set; }

        public AttributeType AttributeType { get; set; }
    }
}
