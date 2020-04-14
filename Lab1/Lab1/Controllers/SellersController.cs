using System;
using Lab1.Data;
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
    public class SellersController : Controller
    {
        private readonly IdentityContext _context;

        public SellersController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Sellers
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Sellers.ToListAsync());
        }*/

        public async Task<IActionResult> Index(int? id, int? productId, string sortOrder)
        {

            var viewModel = new SellerIndexData();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";
            //viewModel.Sellers = from s in _context.Sellers
            //                  select s;
            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Sellers = await _context.Sellers
                   .Include(i => i.SellerProducts).ThenInclude(i => i.Product)
                   .ThenInclude(i => i.Subcategory).ThenInclude(i => i.Category)
                   .AsNoTracking()
                   .OrderByDescending(i => i.Name)
                   .ToListAsync();
                    break;
                case "Rating":
                    viewModel.Sellers = await _context.Sellers
                   .Include(i => i.SellerProducts).ThenInclude(i => i.Product)
                   .ThenInclude(i => i.Subcategory).ThenInclude(i => i.Category)
                   .AsNoTracking()
                   .OrderBy(i => i.Rating)
                   .ToListAsync();
                    break;
                case "rating_desc":
                    viewModel.Sellers = await _context.Sellers
                   .Include(i => i.SellerProducts).ThenInclude(i => i.Product)
                   .ThenInclude(i => i.Subcategory).ThenInclude(i => i.Category)
                   .AsNoTracking()
                   .OrderByDescending(i => i.Name)
                   .ToListAsync();
                    break;
                default:
                    viewModel.Sellers = await _context.Sellers
                   .Include(i => i.SellerProducts).ThenInclude(i => i.Product)
                   .ThenInclude(i => i.Subcategory).ThenInclude(i => i.Category)
                   .AsNoTracking()
                   .OrderBy(i => i.Name)
                   .ToListAsync();
                    break;
            }

            /*viewModel.Sellers = await _context.Sellers
                  .Include(i => i.SellerProducts).ThenInclude(i => i.Product)
                  .ThenInclude(i => i.Subcategory).ThenInclude(i => i.Category)
                  .AsNoTracking()
                  .OrderBy(i => i.Name)
                  .ToListAsync();*/

            if (id != null)
            {
                ViewData["SellerId"] = id.Value;
                Seller seller = viewModel.Sellers.Where(
                    i => i.Id == id.Value).Single();
                viewModel.Products = seller.SellerProducts.Select(s => s.Product).OrderBy(s => s.Name);
                
            }

            if (productId != null)
            {
                ViewData["ProductId"] = productId.Value;
                var selectedProduct = viewModel.Products.Where(p => p.Id == productId).Single();
                viewModel.Reviews = _context.Reviews.Where(c => c.ProductId == selectedProduct.Id);
            }


            //return View(await students.AsNoTracking().ToListAsync());

            return View(viewModel);
        }


        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            var seller = new Seller();
            seller.SellerProducts = new List<SellerProduct>();
            PopulateAssignedProductData(seller);
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Adress,Rating")] Seller seller, string[] selectedProducts)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(seller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seller);*/

            if (selectedProducts != null)
            {
                seller.SellerProducts = new List<SellerProduct>();
                foreach (var product in selectedProducts)
                {
                    var productToAdd = new SellerProduct { SellerId = seller.Id, ProductId = int.Parse(product) };
                    seller.SellerProducts.Add(productToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                var sellers = _context.Sellers.Where(s => s.Name == seller.Name).Where(s => s.Rating == seller.Rating).Where(s => s.Adress == seller.Adress).FirstOrDefault();
                if (sellers != null)
                {
                    ModelState.AddModelError(string.Empty, "This seller already exists");
                    //return View(seller);
                }
                else
                {
                    _context.Add(seller);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            PopulateAssignedProductData(seller);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.Include(i => i.SellerProducts)
                .ThenInclude(i => i.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }
            PopulateAssignedProductData(seller);
            return View(seller);
        }

        /*// POST: Sellers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Adress,Rating")] Seller seller, string[] selectedProducts)
        {
            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedProducts)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sellerToUpdate = await _context.Sellers.Include(s => s.SellerProducts).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Seller>(
                sellerToUpdate,
                "",
                s => s.Name, s => s.Adress, s => s.Rating))
            {
                var sellers = _context.Sellers.Where(s => s.Name == sellerToUpdate.Name).Where(s => s.Rating == sellerToUpdate.Rating).Where(s => s.Adress == sellerToUpdate.Adress).FirstOrDefault();
                if (sellers != null && sellers.Id != sellerToUpdate.Id)
                {
                    ModelState.AddModelError(string.Empty, "This seller already exists");
                    PopulateAssignedProductData(sellerToUpdate);
                    return View(sellerToUpdate);
                }
                else
                {
                    UpdateSellerProducts(selectedProducts, sellerToUpdate);
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
                    return RedirectToAction("Index", "Sellers");
                }
            }
            //PopulateSubcategoriesDropDownList(productToUpdate.SubcategoryId);
            //return View(productToUpdate);
            UpdateSellerProducts(selectedProducts, sellerToUpdate);
            PopulateAssignedProductData(sellerToUpdate);
            return RedirectToAction("Index", "Sellers");
        }

        private void PopulateAssignedProductData(Seller seller)
        {
           
            var allProducts = _context.Products;
            var sellerProducts = new HashSet<int>(seller.SellerProducts.Select(c => c.ProductId));
            
            var viewModel = new List<AssignedProductData>();

            foreach (var product in allProducts)
            {
                viewModel.Add(new AssignedProductData
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Assigned = sellerProducts.Contains(product.Id)
                });
            }
            
            ViewData["Products"] = viewModel;
        }

        private void UpdateSellerProducts(string[] selectedProducts, Seller sellerToUpdate)
        {
            if (selectedProducts == null)
            {
                sellerToUpdate.SellerProducts = new List<SellerProduct>();
                return;
            }

            var selectedProductsHS = new HashSet<string>(selectedProducts);
            var sellerProducts = new HashSet<int>
                (sellerToUpdate.SellerProducts.Select(c => c.Product.Id));
            foreach (var product in _context.Products)
            {
                if (selectedProductsHS.Contains(product.Id.ToString()))
                {
                    if (!sellerProducts.Contains(product.Id))
                    {
                        sellerToUpdate.SellerProducts.Add(new SellerProduct { SellerId = sellerToUpdate.Id, ProductId = product.Id });
                    }
                }
                else
                {

                    if (sellerProducts.Contains(product.Id))
                    {
                        SellerProduct productToRemove = sellerToUpdate.SellerProducts.FirstOrDefault(i => i.ProductId == product.Id);
                        _context.Remove(productToRemove);
                    }
                }
            }
        }


        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var seller = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));*/

            Seller seller = await _context.Sellers
                .Include(i => i.SellerProducts)
                .SingleAsync(i => i.Id == id);

            _context.Sellers.Remove(seller);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}
