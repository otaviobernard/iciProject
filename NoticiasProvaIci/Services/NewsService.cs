using Microsoft.EntityFrameworkCore;
using NoticiasProvaIci.Data;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Services;

public class NewsService(AppDbContext context) : INewsService
{
    public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await context.News
                .Include(n => n.User)
                .Include(n => n.NewsTag)
                .ThenInclude(nt => nt.Tag)
                .ToListAsync();
        }

        public async Task<News?> GetByIdAsync(Guid id)
        {
            return await context.News
                .Include(n => n.NewsTag)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<News> AddUserAsync(News news)
        {
            var user = await context.Users.FindAsync(news.UserId);
            if (user != null)
            {
                news.User = user;
            }
            return news;
        }

        public async Task<News> AddAsync(News news, Guid[] selectedTags)
        {
            context.News.Add(news);
            await context.SaveChangesAsync();

            foreach (var tagId in selectedTags)
            {
                news.NewsTag.Add(new NewsTag { NewsId = news.Id, TagId = tagId });
            }
            await context.SaveChangesAsync();

            return (await context.News
                .Include(n => n.User)
                .Include(n => n.NewsTag)
                .ThenInclude(nt => nt.Tag)
                .FirstOrDefaultAsync(n => n.Id == news.Id))!;
        }

        public async Task<News?> UpdateAsync(News news, Guid[] selectedTags)
        {
            var newsDb = await context.News
                .Include(n => n.NewsTag)
                .FirstOrDefaultAsync(n => n.Id == news.Id);

            if (newsDb == null) return null;

            newsDb.Title = news.Title;
            newsDb.Content = news.Content;
            newsDb.UserId = news.UserId;

            newsDb.NewsTag.Clear();
            foreach (var tagId in selectedTags)
            {
                newsDb.NewsTag.Add(new NewsTag { NewsId = newsDb.Id, TagId = tagId });
            }

            await context.SaveChangesAsync();
            return newsDb;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var news = await context.News.FindAsync(id);
            if (news == null) return false;

            context.News.Remove(news);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetUsersAsync() => await context.Users.ToListAsync();
        
        public async Task<List<Tag>> GetTagsAsync() => await context.Tags.ToListAsync();
}
