using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;

namespace Lab1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IdentityContext _context;

        public ProductsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Products
   
        public async Task<IActionResult> Index(int? id, string? name, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            if(id == 0)
            {
                var hotlineContex = _context.Subcategories.Include(s => s.Category);
                return View(await hotlineContex.ToListAsync());
            }
            ViewBag.SubcategoryId = id;
            ViewBag.SubcategoryName = name;
    
            var productssBySubcategories = _context.Products.Where(b => b.SubcategoryId == id).Include(b => b.Subcategory).AsNoTracking();
            //var productssBySubcategories = _context.Products.Include(p => p.Subcategory).

            var subcategory = _context.Subcategories.Find(id);
            if (name == null)
                ViewBag.SubcategoryName = subcategory.Name;
            ViewBag.CategoryId = subcategory.CategoryId;
            var category = _context.Categories.Find(subcategory.CategoryId);
            ViewBag.CategoryName = category.Name;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
                productssBySubcategories = productssBySubcategories.Where(s => s.Name.Contains(searchString));
            
            switch (sortOrder)
            {
                case "name_desc":
                    productssBySubcategories = productssBySubcategories.OrderByDescending(s => s.Name);
                    break;
                case "Rating":
                    productssBySubcategories = productssBySubcategories.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    productssBySubcategories = productssBySubcategories.OrderByDescending(s => s.Rating);
                    break;
                default:
                    productssBySubcategories = productssBySubcategories.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            ViewBag.SearchString = searchString;

            //var myDatabaseContext = _context.Subcategory.Include(s => s.Category);
            //return View(await productssBySubcategories.ToListAsync());
            return View(await PaginatedList<Product>.CreateAsync(productssBySubcategories.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Index1(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = _context.Products.Include(p => p.Subcategory).AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
                 products = products.Where(s => s.Name.Contains(searchString));

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Rating":
                    products = products.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    products = products.OrderByDescending(s => s.Rating);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            ViewBag.SearchString = searchString;
            //var myDatabaseContext = _context.Subcategory.Include(s => s.Category);
            //return View(await productssBySubcategories.ToListAsync());
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(s => s.SellerProducts).ThenInclude(e => e.Seller).AsNoTracking()
                .Include(p => p.Subcategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;

            return View(product);
        }

        public async Task<IActionResult> ViewComments(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(s => s.SellerProducts).ThenInclude(e => e.Seller).AsNoTracking()
                .Include(p => p.Subcategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Comments", new { Id = product.Id, name = product.Name, num = num, searchString = searchString });
        }

        public async Task<IActionResult> ViewReviews(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(s => s.SellerProducts).ThenInclude(e => e.Seller).AsNoTracking()
                .Include(p => p.Subcategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Reviews", new { Id = product.Id, name = product.Name, num = num, searchString = searchString});
        }

        // GET: Products/Create
        public IActionResult Create(int subcategoryId)
        {  
            //ViewData["SubcategoryId"] = new SelectList(_context.Set<Subcategory>(), "Id", "Name");
            ViewBag.SubcategoryId = subcategoryId;
            ViewBag.SubcategoryName = _context.Subcategories.Where(c => c.Id == subcategoryId).FirstOrDefault().Name;
            var product = new Product();
            product.SellerProducts = new List<SellerProduct>();
            PopulateAssignedSellerData(product);
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Info,Specification,Rating,SubcategoryId")] Product product, string[] selectedSellers)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubcategoryId"] = new SelectList(_context.Set<Subcategory>(), "Id", "Name", product.SubcategoryId);
            return View(product);*/
            if (selectedSellers != null)
            {
                product.SellerProducts = new List<SellerProduct>();
                foreach (var seller in selectedSellers)
                {
                    var sellerToAdd = new SellerProduct { ProductId = product.Id, SellerId = int.Parse(seller) };
                    product.SellerProducts.Add(sellerToAdd);
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var products = _context.Products.Where(p => p.Name == product.Name).Where(p => p.Info == product.Info).Where(p => p.Specification == product.Specification).FirstOrDefault();
                    if (products != null)
                    {
                        ModelState.AddModelError(string.Empty, "This product already exists");
                        return RedirectToAction("Create", "Products", new { subcategoryId = product.SubcategoryId });
                    }
                    else
                    {
                        _context.Add(product);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                        return RedirectToAction("Index", "Products", new { Id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name });
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
            //return View(product);
            PopulateAssignedSellerData(product);
            return RedirectToAction("Index", "Products", new { Id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name });
        }
        
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Subcategory)
                .Include(i => i.SellerProducts)
                .ThenInclude(i => i.Seller)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            //PopulateSubcategoriesDropDownList(product.SubcategoryId);
            ViewData["SubcategoryId"] = new SelectList(_context.Set<Subcategory>(), "Id", "Name", product.SubcategoryId);
            PopulateAssignedSellerData(product);

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;
            return View(product);
        }

        /*// POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Info,Specification,Rating,SubcategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Products", new { id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name });
            }
            //ViewData["SubcategoryId"] = new SelectList(_context.Set<Subcategory>(), "Id", "Name", product.SubcategoryId);
            //return View(product);
            return RedirectToAction("Index", "Products", new { id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name });
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedSellers, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productToUpdate = await _context.Products.Include(s => s.Subcategory)
                .Include(s => s.SellerProducts)
                .ThenInclude(s => s.Seller)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (await TryUpdateModelAsync<Product>(
                productToUpdate,
                "",
                s => s.Name, s => s.Info, s => s.Specification, s => s.Rating, s => s.SubcategoryId))
            {
                var products = _context.Products.Where(p => p.Name == productToUpdate.Name).Where(p => p.Info == productToUpdate.Info).Where(p => p.Specification == productToUpdate.Specification).FirstOrDefault();
                if (products != null && products.Id != productToUpdate.Id)
                {
                    ModelState.AddModelError(string.Empty, "This product already exists");
                    return RedirectToAction("Edit", "Products", new { id = productToUpdate.Id, num = num, searchString = searchString});
                }
                else
                {
                    UpdateProductSellers(selectedSellers, productToUpdate);
                    try
                    {
                        //_context.Update(productToUpdate);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    if(num == 0)
                        return RedirectToAction("Index", "Products", new { Id = productToUpdate.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == productToUpdate.SubcategoryId).FirstOrDefault().Name, num = num, searchString = searchString });
                    else
                        return RedirectToAction("Index1", "Products", new { num = num, searchString = searchString });

                }
            }
            //PopulateSubcategoriesDropDownList(productToUpdate.SubcategoryId);
            //return View(productToUpdate);
            UpdateProductSellers(selectedSellers, productToUpdate);
            PopulateAssignedSellerData(productToUpdate);

            if (num == 0)
                return RedirectToAction("Index", "Products", new { Id = productToUpdate.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == productToUpdate.SubcategoryId).FirstOrDefault().Name, num = num, searchString = searchString });
            else
                return RedirectToAction("Index1", "Products", new { num = num, searchString = searchString });
        }

        private void PopulateSubcategoriesDropDownList(object selectSubcategory = null)
        {
            var subcategoriesQuery = from d in _context.Subcategories
                                  orderby d.Name
                                  select d;
            ViewBag.SubcategoryId = new SelectList(subcategoriesQuery.AsNoTracking(), "Id", "Name", selectSubcategory);
        }

        private void PopulateAssignedSellerData(Product product)
        {

             var allSellers = _context.Sellers;
             var productSellers = new HashSet<int>(product.SellerProducts.Select(c => c.SellerId));
             var viewModel = new List<AssignedSellerData>();

             foreach (var seller in allSellers)
             {
                 viewModel.Add(new AssignedSellerData
                 {
                     SellerId = seller.Id,
                     Name = seller.Name,
                     Assigned = productSellers.Contains(seller.Id)
                 });
             }
             ViewData["Sellers"] = viewModel;
        }

         private void UpdateProductSellers(string[] selectedSellers, Product productToUpdate)
         {
             if (selectedSellers == null)
             {
                 productToUpdate.SellerProducts = new List<SellerProduct>();
                 return;
             }

             var selectedSellersHS = new HashSet<string>(selectedSellers);
             var productSellers = new HashSet<int>
                 (productToUpdate.SellerProducts.Select(c => c.Seller.Id));
             foreach (var seller in _context.Sellers)
             {
                 if (selectedSellersHS.Contains(seller.Id.ToString()))
                 {
                     if (!productSellers.Contains(seller.Id))
                     {
                         productToUpdate.SellerProducts.Add(new SellerProduct { ProductId = productToUpdate.Id, SellerId = seller.Id });
                     }
                 }
                 else
                 {

                     if (productSellers.Contains(seller.Id))
                     {
                         SellerProduct sellerToRemove = productToUpdate.SellerProducts.FirstOrDefault(i => i.SellerId == seller.Id);
                         _context.Remove(sellerToRemove);
                     }
                 }
             }
         }

         // GET: Products/Delete/5
         public async Task<IActionResult> Delete(int? id, int num, string searchString)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var product = await _context.Products
                 .Include(p => p.Subcategory)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (product == null)
             {
                 return NotFound();
             }

            
             ViewBag.Num = num;
             ViewBag.SearchString = searchString;

            return View(product);
         }

     /*// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int num, string searchString)
        {
            Product product = await _context.Products
                .Include(s => s.SellerProducts)
                .SingleAsync(s => s.Id == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Products", new { Id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name });
                if (num == 0)
                    return RedirectToAction("Index", "Products", new { Id = product.SubcategoryId, name = _context.Subcategories.Where(c => c.Id == product.SubcategoryId).FirstOrDefault().Name, num = num, searchString = searchString });
                else
                    return RedirectToAction("Index1", "Products", new { num = num, searchString = searchString });
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
