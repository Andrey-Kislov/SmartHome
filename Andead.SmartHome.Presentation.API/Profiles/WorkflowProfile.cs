using AutoMapper;
using Andead.SmartHome.Presentation.API.Models;

namespace Andead.SmartHome.Presentation.API.Profiles
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            CreateMap<UnitOfWork.Entities.Workflow, WorkflowDto>().ReverseMap();
            CreateMap<UnitOfWork.Entities.WorkflowStep, WorkflowStepDto>().ReverseMap();
        }
    }
}
