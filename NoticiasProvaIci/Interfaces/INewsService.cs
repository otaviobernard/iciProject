using NoticiasProvaIci.Models;

namespace NoticiasProvaIci.Services.Interfaces;

public interface INewsService
{
    Task<IEnumerable<News>> GetAllAsync();
    Task<News?> GetByIdAsync(Guid id);
    Task<News> AddAsync(News news, Guid[] selectedTags);
    Task<News?> UpdateAsync(News news, Guid[] selectedTags);
    Task<bool> DeleteAsync(Guid id);
    Task<List<User>> GetUsersAsync();
    Task<List<Tag>> GetTagsAsync();
    Task<News> AddUserAsync(News news);
}