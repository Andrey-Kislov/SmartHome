using Andead.SmartHome.UnitOfWork.Enums;

namespace Andead.SmartHome.Presentation.API.Models
{
    public class AddAttributeCommand
    {
        public string AttributeFriendlyName { get; set; }

        public string AttributeName { get; set; }

        public int DeviceModelId { get; set; }

        public AttributeType AttributeType { get; set; }
    }
}
