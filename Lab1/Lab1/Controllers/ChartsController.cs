using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IdentityContext _context;

        public ChartsController(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var categories = _context.Categories.Include(b => b.Subcategory).ToList();

            List<object> catSubcat = new List<object>();

            catSubcat.Add(new[] { "Category", "Amount subcategories" });

            foreach(var c in categories)
            {
                catSubcat.Add(new object[] { c.Name, c.Subcategory.Count() });
            }

            return new JsonResult(catSubcat);
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1(string ID)
        {
            //catId = 1;
            var subcategories = _context.Subcategories.Where(s => s.CategoryId == Convert.ToInt32(ID)).Include(b => b.Products).ToList();

            List<object> subcatProducts = new List<object>();

            subcatProducts.Add(new[] { "Subcategory", "Amount products" });

            foreach (var c in subcategories)
            {
                subcatProducts.Add(new object[] { c.Name, c.Products.Count() });
            }

            return new JsonResult(subcatProducts);
        }

        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            //catId = 1;
            var subcategories = _context.Subcategories.Include(b => b.Products).ToList();

            List<object> subcatProducts = new List<object>();

            subcatProducts.Add(new[] { "Subcategory", "Amount products" });

            foreach (var c in subcategories)
            {
                subcatProducts.Add(new object[] { c.Name, c.Products.Count() });
            }

            return new JsonResult(subcatProducts);
        }
    }
}