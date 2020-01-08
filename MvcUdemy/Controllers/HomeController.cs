using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcUdemy.Models;

namespace MvcUdemy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ClientModel _cliente;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _cliente = new ClientModel();
        }

        public IActionResult Index()
        {
            return View();
        }

        //TODO: Move to ClientController
        public IActionResult Client()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateClient(ClientModel client)
        {
            return Json(client.CreateClient(client));
        }

        [HttpGet]
        public JsonResult GetClients()
        {
            return Json(new ClientModel().GetClients());
        }

        [HttpPost]
        public JsonResult RemoveClient(ClientModel client)
        {
            return Json(client.RemmoveClientOfJson(client));
        }
        //--

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
