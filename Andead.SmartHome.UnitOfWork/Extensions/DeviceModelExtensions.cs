using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class DeviceModelExtensions
    {
        public static IQueryBuilder<DeviceModel> ByModelId(this IQueryBuilder<DeviceModel> repository, string modelId)
        {
            return repository.Condition(new Specification<DeviceModel>(x => x.ModelId.ToLower().Equals(modelId.ToLower())));
        }
    }
}
