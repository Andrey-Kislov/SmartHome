using System;

namespace Andead.SmartHome.UnitOfWork.Entities
{
    public class Log : Entity
    {
        public DateTimeOffset DateTime { get; set; }

        public string Message { get; set; }
    }
}
