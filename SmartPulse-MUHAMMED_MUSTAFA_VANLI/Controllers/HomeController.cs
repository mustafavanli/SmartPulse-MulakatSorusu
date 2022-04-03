using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPulse_MUHAMMED_MUSTAFA_VANLI.Models;
using SmartPulse_MUHAMMED_MUSTAFA_VANLI.Services;
using System.Diagnostics;

namespace SmartPulse_MUHAMMED_MUSTAFA_VANLI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransparencyService transparencyService;
        public HomeController(ITransparencyService transparencyService)
        {
            this.transparencyService = transparencyService;
        }

        public async Task<IActionResult> Index()
        {
            DateTime endTime = DateTime.ParseExact("2022-04-03","yyyy-MM-dd",null);
            DateTime starTime = DateTime.ParseExact("2022-04-03", "yyyy-MM-dd", null);


            var list = await transparencyService.GetTradeHistory(starTime, endTime);
            return View(list);
        }
    }
}