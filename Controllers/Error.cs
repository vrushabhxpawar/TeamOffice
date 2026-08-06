using Microsoft.AspNetCore.Mvc;

namespace DemoDownloadPage.Controllers
    {
    public class Error : Controller
        {

        public IActionResult ErrorNotFound ()
            {
            return View();
            }
        }
    }
