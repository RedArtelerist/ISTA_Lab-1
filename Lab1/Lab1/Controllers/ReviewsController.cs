using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Lab1.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<User> _userManager;


        public ReviewsController(IdentityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int? id, string? name, int num, string searchString)
        {
            ViewBag.ProductId = id;
            ViewBag.ProductName = name;

            var product = _context.Products.Find(id);
            if (name == null)
                ViewBag.ProductName = name;

            ViewBag.SubcategoryId = product.SubcategoryId;
            var subcategory = _context.Subcategories.Find(product.SubcategoryId);
            ViewBag.SubcategoryName = subcategory.Name;

            var reviewsByProducts = _context.Reviews.Where(b => b.ProductId == id).Include(b => b.Product);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                User user = await _userManager.FindByIdAsync(userId);
                ViewBag.UserId = userId;
                ViewBag.UserName = user.Nick;

                var review = _context.Reviews.Where(b => b.UserId == userId).Include(b => b.User).Where(c => c.ProductId == id).Include(c => c.Product);

                if (review.Count() != 0)
                {
                    ViewBag.Count = 1;
                }
                else
                    ViewBag.Count = 0;

            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;
            ViewBag.UserManager = _userManager;

            return View(await reviewsByProducts.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.ProductId = review.ProductId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name;

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int productId, string? userId, string? userName, int num, string searchString)
        {
            if (userId != null)
            {
                ViewBag.ProductId = productId;
                ViewBag.ProductName = _context.Products.Where(c => c.Id == productId).FirstOrDefault().Name;

                var product = _context.Products.Find(productId);
                ViewBag.SubcategoryId = product.SubcategoryId;
                var subcategory = _context.Subcategories.Find(product.SubcategoryId);
                ViewBag.SubcategoryName = subcategory.Name;

                ViewBag.UserId = userId;
                ViewBag.UserName = userName;

                ViewBag.Num = num;
                ViewBag.SearchString = searchString;

                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Info,Date,Rating,ProductId,UserId,UserName")] Review review, int num, string searchString)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(review);

                    User user = await _userManager.FindByIdAsync(review.UserId);
                    user.Review.Add(review);

                    _context.Update(user);

                    await _context.SaveChangesAsync();

                    var product = _context.Products.Find(review.ProductId);
                    var reviews = _context.Reviews.Where(c => c.ProductId == product.Id);

                    //int count = product.Review.Count();
                    double? rating = reviews.Average(c => c.Rating);

                    product.Rating = rating;

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Reviews", new { Id = review.ProductId, name = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
                }
                else
                {
                    return RedirectToAction("Create", "Reviews", new { productId = review.ProductId, userId = review.UserId, num = num, searchString = searchString });

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
            //return View(subcategory);
            return RedirectToAction("Index", "Reviews", new { Id = review.ProductId, name = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name });
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id, int productId, string userId, string userName, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            ViewBag.ProductId = productId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == productId).FirstOrDefault().Name;
            ViewBag.UserId = userId;
            ViewBag.UserName = userName;

            if (review == null)
            {
                return NotFound();
            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;

            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", review.ProductId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Info,Date,Rating,ProductId,UserId,UserName")] Review review, int num, string searchString)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);

                    User user = await _userManager.FindByIdAsync(review.UserId);
                    user.Review.Where(c => c.Id == review.Id).FirstOrDefault().Info = review.Info;
                    user.Review.Where(c => c.Id == review.Id).FirstOrDefault().Date = review.Date;
                    user.Review.Where(c => c.Id == review.Id).FirstOrDefault().Rating = review.Rating;


                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    var product = _context.Products.Find(review.ProductId);
                    var reviews = _context.Reviews.Where(c => c.ProductId == product.Id);

                    //int count = product.Review.Count();
                    double? rating = reviews.Average(c => c.Rating);

                    product.Rating = rating;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Reviews", new { Id = review.ProductId, name = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
            }
            else
            {
                return RedirectToAction("Edit", "Reviews", new { id = review.Id, productId = review.ProductId, userId = review.UserId, num = num, searchString = searchString });

            }
            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", review.ProductId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", review.UserId);
            //return RedirectToAction("Index", "Reviews", new { Id = review.ProductId, name = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name });
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ProductId = review.ProductId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name;

            if (review == null)
            {
                return NotFound();
            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int num, string searchString)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);

            User user = await _userManager.FindByIdAsync(review.UserId);
            user.Review.Remove(user.Review.Where(c => c.Id == review.Id).FirstOrDefault());

            _context.Update(user);

            await _context.SaveChangesAsync();

            var product = _context.Products.Find(review.ProductId);
            var reviews = _context.Reviews.Where(c => c.ProductId == product.Id);
            if (reviews.Count() == 0)
            {
                product.Rating = 0;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                //int count = product.Review.Count();
                double? rating = reviews.Average(c => c.Rating);

                product.Rating = rating;

                _context.Update(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Reviews", new { Id = review.ProductId, name = _context.Products.Where(c => c.Id == review.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
