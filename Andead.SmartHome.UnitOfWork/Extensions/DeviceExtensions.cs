using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class DeviceExtensions
    {
        public static IQueryBuilder<Device> ByIeeeAddress(this IQueryBuilder<Device> repository, string ieeeAddress)
        {
            return repository.Condition(new Specification<Device>(x => x.IeeeAddress.ToLower().Equals(ieeeAddress.ToLower())));
        }

        public static IQueryBuilder<Device> ByUserId(this IQueryBuilder<Device> repository, int userId)
        {
            return repository.Condition(new Specification<Device>(x => x.UserId == userId));
        }
    }
}
