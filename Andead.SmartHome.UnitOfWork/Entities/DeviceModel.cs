using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class DeviceModel : Entity
    {
        public string ModelName { get; set; }

        public string ModelId { get; set; }

        public string ImageUrl { get; set; }

        public virtual IList<DeviceAttribute> Attributes { get; set; }
    }
}
