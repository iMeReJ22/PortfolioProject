using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.API.Controllers
{
    public class ActivityLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
