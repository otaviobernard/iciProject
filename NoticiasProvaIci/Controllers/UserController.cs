using Microsoft.AspNetCore.Mvc;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Controllers
{
    public class UserController(IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetAllAsync();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await userService.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, User user)
        {
            if (ModelState.IsValid)
            {
                await userService.UpdateAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
