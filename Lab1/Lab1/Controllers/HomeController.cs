using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab1.Models;
using Lab1.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IdentityContext _context;

        public HomeController(IdentityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> About(string button, string ChartType)
        {
            var products = _context.Products;
            ViewBag.ProductCount = products.Count();
            var sellers = _context.Sellers;
            ViewBag.SellerCount = sellers.Count();

            if (button == "first")
            {
                ViewBag.Button = '1';
            }
            else if (button == "second")
                ViewBag.Button = '2';

            ViewBag.Chart = ChartType;

            //if (ChartType != null)
            return View();

            //return RedirectToAction("SelectChart");
        }

        public ActionResult SelectChart(string button)
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "PieChart", Value = "0", Selected = true });
            items.Add(new SelectListItem { Text = "BarChart", Value = "1" });
            items.Add(new SelectListItem { Text = "ColumnChart", Value = "2" });
            items.Add(new SelectListItem { Text = "LineChart", Value = "3" });

            ViewBag.ChartType = items;
            ViewBag.Button = button;

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
