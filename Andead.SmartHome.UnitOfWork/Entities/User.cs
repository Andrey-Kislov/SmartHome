using System.Collections.Generic;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IList<Device> Devices { get; set; }
    }
}
