using NoticiasProvaIci.Models;

namespace NoticiasProvaIci.Services.Interfaces;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllAsync();
    Task<Tag?> GetByIdAsync(Guid id);
    Task<Tag> AddAsync(Tag tag);
    Task<Tag?> UpdateAsync(Tag tag);
    Task<bool> DeleteAsync(Guid id);
}