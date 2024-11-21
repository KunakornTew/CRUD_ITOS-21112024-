using Microsoft.AspNetCore.Mvc;

namespace ITOS_LEARN.Controllers
{
    public class WaitingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
