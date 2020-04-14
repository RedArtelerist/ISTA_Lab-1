using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;
using System.Drawing;

namespace Lab1.Controllers
{
    public class SubcategoriesController : Controller
    {
        private readonly IdentityContext _context;

        private struct S
        {
            public string worksheet;
            public List<int> lines;
        }

        public SubcategoriesController(IdentityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? Id, string? name, string formatError, string fieldError, string valueError)
        {
            //if (id == null) return RedirectToAction("Index", "Categories");
            ViewBag.CategoryId = Id;
            ViewBag.CategoryName = name;
            var subcategoriesByCategories = _context.Subcategories.Where(b => b.CategoryId == Id).Include(b => b.Category);
            //var myDatabaseContext = _context.Subcategory.Include(s => s.Category);
            if (formatError != "" && formatError != null && formatError != string.Empty)
                ViewBag.FormatError = formatError;
            if (fieldError != "" && fieldError != null && fieldError != string.Empty)
                ViewBag.FieldError = "There are some errors with worksheets' names or their fields, so they were skipped. " + fieldError;
            if (valueError != "" && valueError != null && valueError != string.Empty)
                ViewBag.ValueError = "There are some value errors. " + valueError;

            return View(await subcategoriesByCategories.ToListAsync());
        }

        // GET: Subcategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            //return View(subcategory);
            return RedirectToAction("Index", "Products", new { Id = subcategory.Id, name = subcategory.Name });

        }

        // GET: Subcategories/Create
        public IActionResult Create(int categoryId)
        {
            //ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name;
            //PopulateDepartmentsDropDownList();
            return View();
        }
        /*public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }*/

        // POST: Subcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId")] Subcategory subcategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subcategories = _context.Subcategories.Where(s => s.Name == subcategory.Name).FirstOrDefault();
                    if (subcategories != null)
                    {
                        ModelState.AddModelError(string.Empty, "This subcategory already exists");
                        return RedirectToAction("Create", "Subcategories", new { categoryId = subcategory.CategoryId });
                    }
                    else
                    {
                        _context.Add(subcategory);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                        return RedirectToAction("Index", "Subcategories", new { Id = subcategory.CategoryId, name = _context.Categories.Where(c => c.Id == subcategory.CategoryId).FirstOrDefault().Name });
                    }
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            //PopulateDepartmentsDropDownList(subcategory.CategoryId);
            return View(subcategory);
            //return RedirectToAction("Index", "Subcategories", new { id = subcategory.CategoryId, name = _context.Categories.Where(c => c.Id == subcategory.CategoryId).FirstOrDefault().Name });
        }

        // GET: Subcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories.FindAsync(id);
            //var subcategory = await _context.Subcategories
            //.AsNoTracking()
            //.FirstOrDefaultAsync(m => m.Id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", subcategory.CategoryId);
            //PopulateDepartmentsDropDownList(subcategory.CategoryId);
            return View(subcategory);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] Subcategory subcategory)
        {
            if (id != subcategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var subcategories = _context.Subcategories.Where(s => s.Name == subcategory.Name).FirstOrDefault();

                    if (subcategories != null)
                    {
                        ModelState.AddModelError(string.Empty, "This subcategory already exists");
                        return RedirectToAction("Edit", "Subcategories", new { id = subcategory.Id });
                    }
                    else
                    {
                        _context.Update(subcategory);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategoryExists(subcategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Subcategories", new { Id = subcategory.CategoryId, name = _context.Categories.Where(c => c.Id == subcategory.CategoryId).FirstOrDefault().Name });

            }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", subcategory.CategoryId);
            //return View(subcategory);
            return RedirectToAction("Index", "Subcategories", new { Id = subcategory.CategoryId, name = _context.Categories.Where(c => c.Id == subcategory.CategoryId).FirstOrDefault().Name });

        }
        /*[HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategoryToUpdate = await _context.Subcategories.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Subcategory>(
                subcategoryToUpdate,
                "",
                s => s.Name, s => s.CategoryId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "Subcategories", new { id = subcategoryToUpdate.CategoryId, name = _context.Categories.Where(c => c.Id == subcategoryToUpdate.CategoryId).FirstOrDefault().Name });
                }
                catch (DbUpdateException /* ex )
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            //return View(subcategoryToUpdate);
            //PopulateDepartmentsDropDownList(subcategoryToUpdate.CategoryId);
            return RedirectToAction("Index", "Subcategories", new { id = subcategoryToUpdate.CategoryId, name = _context.Categories.Where(c => c.Id == subcategoryToUpdate.CategoryId).FirstOrDefault().Name });

        }*/

        private void PopulateDepartmentsDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                   orderby d.Name
                                   select d;
            ViewBag.CategoryId = new SelectList(categoriesQuery.AsNoTracking(), "Id", "Name", selectedCategory);
        }

        // GET: Subcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories
                .Include(s => s.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategory == null)
            {
                return NotFound();
            }

            return View(subcategory);
        }

        /*// POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Subcategories.Remove(subcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Subcategories", new { Id = subcategory.CategoryId, name = _context.Categories.Where(c => c.Id == subcategory.CategoryId).FirstOrDefault().Name });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool SubcategoryExists(int id)
        {
            return _context.Subcategories.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel, int categoryId)
        {
            List<S> errors = new List<S>();
            int h = 0;
            string FieldErrors = "";
            string ValueError = "";
            Category category = _context.Categories.Find(categoryId);
            if (ModelState.IsValid)
            {
                try
                {
                    if (fileExcel.FileName == "" || fileExcel.FileName == null || fileExcel.FileName == string.Empty)
                        throw new NullReferenceException();
                    if (!fileExcel.FileName.Contains(".xlsx"))
                        throw new FileFormatException();
                    if (fileExcel != null)
                    {
                        using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                        {
                            await fileExcel.CopyToAsync(stream);
                            using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                            {
                                //перегляд усіх листів (в даному випадку категорій)
                                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                                {
                                    errors.Add(new S { worksheet = worksheet.Name, lines = new List<int>() });

                                    //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
                                    Subcategory newsubcat;

                                    if (!CountRu(worksheet.Name))
                                    {
                                        FieldErrors += "Error in " + worksheet.Name + " worksheet; ";
                                        h++;
                                        continue;
                                    }

                                    string worksheetName = worksheet.Name.ToLower();

                                    var s = (from subcat in _context.Subcategories
                                             where subcat.Name.ToLower().Contains(worksheetName)
                                             select subcat).ToList();

                                    if (s.Count > 0)
                                    {
                                        newsubcat = s[0];
                                    }
                                    else
                                    {
                                        newsubcat = new Subcategory();
                                        newsubcat.Name = worksheet.Name;
                                        newsubcat.CategoryId = categoryId;
                                        _context.Subcategories.Add(newsubcat);
                                    }

                                    if (worksheet.Row(1).Cell(1).Value.ToString() != "Name")
                                    {
                                        FieldErrors += "Error in " + worksheet.Name + " worksheet; ";
                                        h++;
                                        continue;
                                    }
                                    if (worksheet.Row(1).Cell(2).Value.ToString() != "Info")
                                    {
                                        FieldErrors += "Error in " + worksheet.Name + " worksheet; ";
                                        h++;
                                        continue;
                                    }
                                    if (worksheet.Row(1).Cell(3).Value.ToString() != "Specification")
                                    {
                                        FieldErrors += "Error in " + worksheet.Name + " worksheet; ";
                                        h++;
                                        continue;
                                    }
                                    if (worksheet.Row(1).Cell(4).Value.ToString() != "Rating")
                                    {
                                        FieldErrors += "Error in " + worksheet.Name + " worksheet; ";
                                        h++;
                                        continue;
                                    }
                                    int l = 5;

                                    try
                                    {
                                        while (true)
                                        {
                                            string cell = worksheet.Row(1).Cell(l).Value.ToString();
                                            if (cell == null || cell == "" || cell == string.Empty)
                                                break;
                                            if (!cell.Contains("Seller") && !cell.Contains("seller"))
                                                throw new ArgumentException();
                                            l++;
                                        }
                                    }
                                    catch(ArgumentException a)
                                    {
                                        FieldErrors += "Error in " + worksheetName + " worksheet; ";
                                        h++;
                                        continue;
                                    }
                                    //перегляд усіх рядків                    
                                    foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                    {
                                        try
                                        {
                                            Product product = new Product();
                                            product.Name = row.Cell(1).Value.ToString();

                                            if (!CountRu(product.Name))
                                                throw new Exception();

                                            product.Info = row.Cell(2).Value.ToString();

                                            product.Specification = row.Cell(3).Value.ToString();

                                            if (Convert.ToDouble(row.Cell(4).Value) > 10 || Convert.ToDouble(row.Cell(4).Value) < 0)
                                                throw new Exception();

                                            product.Rating = Convert.ToDouble(row.Cell(4).Value);
                                            product.Subcategory = newsubcat;

                                            if (product.Name == null || product.Specification == null || product.Rating == null)
                                                throw new Exception();

                                            string name = product.Name.ToLower();

                                            //string Name = Char.ToUpper(name[0]) + name.Substring(1);

                                            Product p = _context.Products.Where(p => p.Name.ToLower() == name).FirstOrDefault();

                                            if (p != null)
                                            {
                                                p.Info = product.Info;
                                                p.Specification = product.Specification;
                                                p.Rating = product.Rating;
                                                p.Subcategory = product.Subcategory;
                                                for (int i = 5; i <= l; i++)
                                                {
                                                    if (row.Cell(i).Value.ToString().Length > 0)
                                                    {
                                                        Seller seller;

                                                        var a = (from aut in _context.Sellers
                                                                 where aut.Name.ToLower().Contains(row.Cell(i).Value.ToString().ToLower())
                                                                 select aut).ToList();

                                                        if (a.Count > 0)
                                                        {
                                                            seller = a[0];

                                                            var selpr = (from o in _context.SellerProducts
                                                                         where o.ProductId == p.Id && o.SellerId == seller.Id
                                                                         select o).ToList();
                                                            if (selpr.Count > 0)
                                                                continue;

                                                            SellerProduct sp = new SellerProduct();
                                                            sp.Product = p;
                                                            sp.Seller = seller;
                                                            _context.SellerProducts.Add(sp);

                                                        }
                                                        else
                                                        {
                                                            seller = new Seller();
                                                            seller.Name = row.Cell(i).Value.ToString();
                                                            seller.Adress = "FromExcel";

                                                            if (!CountRu(seller.Name))
                                                            {
                                                                errors[h].lines.Add(row.RowNumber());
                                                                continue;
                                                            }

                                                            //додати в контекст
                                                            _context.Add(seller);

                                                            SellerProduct sp = new SellerProduct();
                                                            sp.Product = p;
                                                            sp.Seller = seller;
                                                            _context.SellerProducts.Add(sp);
                                                        }

                                                    }
                                                }
                                                _context.Update(p);
                                            }
                                            else
                                            {
                                                _context.Products.Add(product);

                                                for (int i = 5; i <= l; i++)
                                                {
                                                    if (row.Cell(i).Value.ToString().Length > 0)
                                                    {
                                                        Seller seller;

                                                        var a = (from aut in _context.Sellers
                                                                 where aut.Name.ToLower().Contains(row.Cell(i).Value.ToString().ToLower())
                                                                 select aut).ToList();

                                                        if (a.Count > 0)
                                                        {
                                                            seller = a[0];
                                                        }
                                                        else
                                                        {
                                                            seller = new Seller();
                                                            seller.Name = row.Cell(i).Value.ToString();
                                                            seller.Adress = "FromExcel";

                                                            if (!CountRu(seller.Name))
                                                            {
                                                                errors[h].lines.Add(row.RowNumber());
                                                                continue;
                                                            }
                                                            //throw new Exception();

                                                            _context.Add(seller);
                                                        }
                                                        SellerProduct sp = new SellerProduct();
                                                        sp.Product = product;
                                                        sp.Seller = seller;
                                                        _context.SellerProducts.Add(sp);
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            errors[h].lines.Add(row.RowNumber());
                                        }
                                    }
                                    h++;
                                }
                            }
                        }
                    }
                }
                catch(FileFormatException)
                {
                    return RedirectToAction("Index", "Subcategories", new { name = category.Name, Id = categoryId, formatError = "Incorrect File Format" });
                }
                catch(NullReferenceException)
                {
                    return RedirectToAction("Index", "Subcategories", new { name = category.Name, Id = categoryId, formatError = "Error! Choose file" });
                }

                await _context.SaveChangesAsync();
            }

            foreach(var e in errors)
            {
                if (e.lines.Count() == 0)
                    continue;
                ValueError += e.worksheet + ": ";
                foreach (var line in e.lines)
                    ValueError += "line " + line.ToString() + "; ";
            }

            return RedirectToAction("Index", "Subcategories", new { name = category.Name, Id = categoryId , fieldError = FieldErrors, valueError = ValueError});
        }

        private bool CountRu(string str)
        {
            int Ru = 0;
            if (str[0] >= '0' && str[0] <= '9')
                return false;
            for (int g = 0; g < str.Length; g++)
                if ((str[g] >= 'А' && str[g] <= 'Я') || (str[g] >= 'а' && str[g] <= 'я'))
                    Ru++;
            if (Ru > 0) return false;
            return true;
        }

        public ActionResult Export(int categoryId, int[] subcats)
        {
            var category = _context.Categories.Find(categoryId);
            if (subcats.Length == 0)
                return RedirectToAction("Index", "Subcategories", new { Id = categoryId, name = category.Name});

            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var subcategories = _context.Subcategories.Where(s => s.CategoryId == categoryId).Include("Products").ToList();
                List<Subcategory> subs = new List<Subcategory>();

                foreach(int sub in subcats)
                {
                    subs.Add(subcategories.Where(s => s.Id == sub).FirstOrDefault());
                }

                foreach (var s in subs)
                {
                    var worksheet = workbook.Worksheets.Add(s.Name);

                    worksheet.Cell("A1").Value = "Name";
                    worksheet.Cell("B1").Value = "Info";
                    worksheet.Cell("C1").Value = "Specification";
                    worksheet.Cell("D1").Value = "Rating";

                    int n = _context.Sellers.ToList().Count();

                    for (int i = 0; i <= n; i++)
                        worksheet.Row(1).Cell(i + 5).Value = "Seller " + (i + 1).ToString();
                    
                    worksheet.Row(1).Style.Font.Bold = true;

                    var products = s.Products.OrderBy(p => p.Name).ToList();

                    for (int i = 1; i <= n + 5; i++)
                        worksheet.Row(1).Cell(i).Style.Fill.BackgroundColor = XLColor.Orange;

                    for (int i = 0; i < products.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = products[i].Name;
                        worksheet.Cell(i + 2, 2).Value = products[i].Info;
                        worksheet.Cell(i + 2, 3).Value = products[i].Specification;
                        worksheet.Cell(i + 2, 4).Value = products[i].Rating;

                        var sp = _context.SellerProducts.Where(a => a.ProductId == products[i].Id).OrderBy(a => a.Seller.Name).ToList();
                        if (sp.Count == 0)
                            continue;

                        int j = 5;
                        foreach (var a in sp)
                        {
                            worksheet.Cell(i + 2, j).Value = a.Seller.Name;
                            j++;
                        }
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"library_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };

                }
            }
        }
    }
}
