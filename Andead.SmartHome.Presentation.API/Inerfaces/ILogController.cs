using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Andead.SmartHome.Presentation.API.Inerfaces
{
    public interface ILogController
    {
        Task<IActionResult> Add(string message);

        IActionResult Get();

        IActionResult GetById(int id);
    }
}
