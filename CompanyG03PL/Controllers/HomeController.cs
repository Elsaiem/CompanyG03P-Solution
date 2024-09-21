using CompanyG03PL.Models;
using CompanyG03PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace CompanyG03PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IScoopedService scoopedService1;
        //private readonly IScoopedService scoopedService2;
        //private readonly ITransientService transientService1;
        //private readonly ITransientService transientService2;
        //private readonly ISingeletonService service1;
        //private readonly ISingeletonService service2;

        public HomeController(ILogger<HomeController> logger/*, IScoopedService scoopedService1,*/
        //IScoopedService scoopedService2,
        //ITransientService transientService1,
        //ITransientService transientService2,
        //ISingeletonService service1,
        //ISingeletonService service2
            )
        {
            _logger = logger;
            //this.scoopedService1 = scoopedService1;
            //this.scoopedService2 = scoopedService2;
            //this.transientService1 = transientService1;
            //this.transientService2 = transientService2;
            //this.service1 = service1;
            //this.service2 = service2;
        }
        //public string TestServises()
        //{
        //    StringBuilder stringBuilder= new StringBuilder();
        //    stringBuilder.Append($"scoopedService1 :: {scoopedService1.getGuid()}\n");
        //    stringBuilder.Append($"scoopedService2 :: {scoopedService2.getGuid()}\n");

        //    stringBuilder.Append($"transientService1 :: {transientService1.getGuid()}\n");
        //    stringBuilder.Append($"transientService2 :: {transientService2.getGuid()}\n");


        //    stringBuilder.Append($"service1 :: {service1.getGuid()}\n");
        //    stringBuilder.Append($"service2 :: {service2.getGuid()}\n");
            
        //    return stringBuilder.ToString();
        //}


        public IActionResult Index()
        {
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
