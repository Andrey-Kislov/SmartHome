using Microsoft.AspNetCore.Mvc;

namespace Andead.SmartHome.Presentation.API.Inerfaces
{
    public interface ILogController
    {
        IActionResult Add(string message);

        IActionResult Get();

        IActionResult GetById(long id);
    }
}
