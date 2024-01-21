using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestApp.Data.interfaces;
using TestApp.Data.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAllJsons _allJsons;

        public HomeController(ILogger<HomeController> logger, IAllJsons allJsons)
        {
            _logger = logger;
            _allJsons = allJsons;
        }

        public IActionResult Index()
        {
            var data = _allJsons.JsonNames;
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
