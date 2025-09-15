using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Controllers
{ 
    public class NewsController(INewsService newsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var news = await newsService.GetAllAsync();
            return View(news);
        }

        public async Task<IActionResult> Create()
        {
            if (!await LoadUsersAndTagsAsync()) 
                return NewsValidation(Constants.NoTagsOrUsersMessage);

            return View(new News());
        }

        [HttpPost]
        public async Task<IActionResult> Create(News news, Guid[] selectedTags)
        {
            if (news.User is null)
                news = await newsService.AddUserAsync(news);

            if (!selectedTags.Any())
                ModelState.AddModelError("Tags", "Selecione ao menos uma Tag.");

            if (ModelState.IsValid)
            {
                await newsService.AddAsync(news, selectedTags);
                return RedirectToAction(nameof(Index));
            }

            await LoadUsersAndTagsAsync();
            return View(news);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var news = await newsService.GetByIdAsync(id);
            if (news == null) return NotFound();

            await LoadUsersAndTagsAsync();
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(News news, Guid[] selectedTags)
        {
            if (selectedTags == null || !selectedTags.Any())
                ModelState.AddModelError("Tags", "Selecione ao menos uma Tag.");

            if (ModelState.IsValid)
            {
                var updated = await newsService.UpdateAsync(news, selectedTags);
                if (updated == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }

            await LoadUsersAndTagsAsync();
            return View(news);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await newsService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LoadUsersAndTagsAsync()
        {
            var tags = await newsService.GetTagsAsync();
            var users = await newsService.GetUsersAsync();

            if (!tags.Any() || !users.Any()) return false;

            ViewBag.Tags = new SelectList(tags, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Name");
            return true;
        }

        private RedirectToActionResult NewsValidation(string message)
        {
            TempData["PopupMessage"] = message;
            return RedirectToAction(nameof(Index));
        }
    }
}