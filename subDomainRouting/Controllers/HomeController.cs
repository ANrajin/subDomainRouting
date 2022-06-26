using Microsoft.AspNetCore.Mvc;
using subDomainRouting.Models;
using System.Diagnostics;

namespace subDomainRouting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var entry = "127.0.0.1\ttest.localhost";
            var hostFile = new FileInfo(Path.Combine(Environment.
                GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts"));

            using (StreamReader sr = hostFile.OpenText())
            {
                var lines = sr.ReadToEnd();

                if(lines.Contains(entry))
                {
                    Console.WriteLine("Ache");
                }
                else
                {
                    try
                    {
                        using (StreamWriter sw = hostFile.AppendText())
                        {
                            sw.WriteLine(entry);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return View();
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