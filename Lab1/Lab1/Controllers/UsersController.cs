using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Data;
using Lab1.Models;
using Lab1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class UsersController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        IdentityContext _context;

        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserManager = _userManager;
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Nick = model.Nick };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    List<string> role = new List<string>() { "user" };
                    await _userManager.AddToRolesAsync(user, role);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Nick = user.Nick };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //User u = new User { Email = model.Email, UserName = model.Email, Nick = model.Nick };
                User user = await _userManager.FindByIdAsync(model.Id);
                User resUser = user;
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Nick = model.Nick;

                    //await _context.SaveChangesAsync();

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        user = resUser;

                        var comments = _context.Comments.Where(c => c.UserId == user.Id).ToList();
                        var rewiews = _context.Reviews.Where(r => r.UserId == user.Id).ToList();
                        foreach (var com in comments)
                        {
                            com.UserName = model.Nick;
                            _context.Update(com);
                        }

                        foreach (var rew in rewiews)
                        {
                            rew.UserName = model.Nick;
                            _context.Update(rew);
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var comments = _context.Comments.Where(c => c.UserId == user.Id).ToList();
                var rewiews = _context.Reviews.Where(r => r.UserId == user.Id).ToList();
                foreach (var com in comments)
                    _context.Comments.Remove(com);

                foreach (var rew in rewiews)
                    _context.Reviews.Remove(rew);

                await _context.SaveChangesAsync();

                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found");
                }
            }
            return View(model);
        }
    }
}