using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CachingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60,
            Location = ResponseCacheLocation.Client)]
        public IActionResult MemoriaDistribuida()
        {
            string fecha =
                DateTime.Now.ToLongDateString() + " -- "
                + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}
