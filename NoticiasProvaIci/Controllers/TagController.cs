using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoticiasProvaIci.Data;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Controllers
{
    public class TagController(ITagService tagService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var tags = await tagService.GetAllAsync();
            return View(tags);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
                return View(tag);

            await tagService.AddAsync(tag);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagService.GetByIdAsync(id);
            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (!ModelState.IsValid)
                return View(tag);

            var updated = await tagService.UpdateAsync(tag);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await tagService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}