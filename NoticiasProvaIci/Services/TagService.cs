using Microsoft.EntityFrameworkCore;
using NoticiasProvaIci.Data;
using NoticiasProvaIci.Models;
using NoticiasProvaIci.Services.Interfaces;

namespace NoticiasProvaIci.Services;

public class TagService(AppDbContext context) : ITagService
{
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(Guid id)
    {
        return await context.Tags.FindAsync(id);
    }

    public async Task<Tag> AddAsync(Tag tag)
    {
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> UpdateAsync(Tag tag)
    {
        var existing = await context.Tags.FindAsync(tag.Id);
        if (existing == null) return null;

        existing.Name = tag.Name;
        context.Tags.Update(existing);
        await context.SaveChangesAsync();

        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tag = await context.Tags.FindAsync(id);
        if (tag == null) return false;

        context.Tags.Remove(tag);
        await context.SaveChangesAsync();
        return true;
    }
}
