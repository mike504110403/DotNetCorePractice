using Microsoft.AspNetCore.Mvc;
using MyDotNetCoreCodeBase.Middleware;
using MyDotNetCoreCodeBase.Models;
using System.Diagnostics;
using static MyDotNetCoreCodeBase.Middleware.Piepeline;

namespace MyDotNetCoreCodeBase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // 透過加屬性filter 進middleware
        [MiddlewareFilter(typeof(CustomerExceptionPipeline))]
        public IActionResult Index()
        {
            throw new Exception();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}