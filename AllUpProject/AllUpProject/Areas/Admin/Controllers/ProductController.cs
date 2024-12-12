using Microsoft.AspNetCore.Mvc;

namespace AllUpProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
