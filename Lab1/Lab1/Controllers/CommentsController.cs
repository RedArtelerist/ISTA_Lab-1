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
    public class CommentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<User> _userMenager;
        private int Num;
        private string SearchString;

        public CommentsController(IdentityContext context, UserManager<User> userMenager)
        {
            _context = context;
            _userMenager = userMenager;
        }

        // GET: Comments
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

            var commentsByProducts = _context.Comments.Where(b => b.ProductId == id).Include(b => b.Product);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                User user = await _userMenager.FindByIdAsync(userId);
                ViewBag.UserId = userId;
                ViewBag.UserName = user.Nick;
            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;
            ViewBag.UserManager = _userMenager;

            return View(await commentsByProducts.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Product)
                .Include(r => r.Us)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.ProductId = comment.ProductId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name;

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
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

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Info,Date,ProductId,UserId,UserName")] Comment comment, int num, string searchString)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(comment);

                    //User user = await _userMenager.FindByIdAsync(comment.UserId);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Comments", new { Id = comment.ProductId, name = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
                }
                else
                {
                    return RedirectToAction("Create", "Comments", new { productId = comment.ProductId, userId = comment.UserId, num = num, searchString = searchString });
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
            return RedirectToAction("Index", "Comments", new { Id = comment.ProductId, name = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id, int productId, string userId, string userName, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            ViewBag.ProductId = productId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == productId).FirstOrDefault().Name;
            ViewBag.UserId = userId;
            ViewBag.UserName = userName;

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;

            if (comment == null)
            {
                return NotFound();
            }
            //ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "Id", "Name", comment.ProductId);
         
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Info, Date, ProductId, UserId, UserName")] Comment comment, int num, string searchString)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);

                    User user = await _userMenager.FindByIdAsync(comment.UserId);
                    user.Comment.Where(c => c.Id == comment.Id).FirstOrDefault().Info = comment.Info;
                    user.Comment.Where(c => c.Id == comment.Id).FirstOrDefault().Date = comment.Date;

                    _context.Update(user);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Comments", new { Id = comment.ProductId, name = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
            }
            else
            {
                return RedirectToAction("Edit", "Comments", new { id = comment.Id, productId = comment.ProductId, userId = comment.UserId, num = num, searchString = searchString });
            }
            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", comment.ProductId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", comment.UserId);
            ////return View(comment);
            //return RedirectToAction("Index", "Comments", new { Id = comment.ProductId, name = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name });
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id, int num, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Product)
                .Include(c => c.Us)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.ProductId = comment.ProductId;
            ViewBag.ProductName = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name;

            if (comment == null)
            {
                return NotFound();
            }

            ViewBag.Num = num;
            ViewBag.SearchString = searchString;

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int num, string searchString)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);

            User user = await _userMenager.FindByIdAsync(comment.UserId);
            user.Comment.Remove(user.Comment.Where(c => c.Id == comment.Id).FirstOrDefault());
           
            _context.Update(user);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Comments", new { Id = comment.ProductId, name = _context.Products.Where(c => c.Id == comment.ProductId).FirstOrDefault().Name, num = num, searchString = searchString });
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
