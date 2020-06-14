using Andead.SmartHome.UnitOfWork.Enums;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class DeviceAttribute : Entity
    {
        public string AttributeFriendlyName { get; set; }

        public string AttributeName { get; set; }

        public virtual DeviceModel DeviceModel { get; set; }

        public int DeviceModelId { get; set; }

        public AttributeType AttributeType { get; set; }

        public AttributeValueType AttributeValueType { get; set; }
    }
}
