using Microsoft.AspNetCore.Mvc;
using TestApp.Data.interfaces;

namespace TestApp.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IAllJsons _allJsons;

        public OverviewController(IAllJsons allJsons)
        {
            _allJsons = allJsons;
        }

        [Route("overview")]
        [Route("overview/{id}")]
        public IActionResult Index(int id)
        {
            var data = _allJsons.GetUploadJson(id);
            return View(data);
        }
    }
}
