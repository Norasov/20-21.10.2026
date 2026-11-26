using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;
    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddGenreAsync(Genre genre)
{
    await _context.Genres.AddAsync(genre);
    return await _context.SaveChangesAsync() > 0;
}

public async Task<bool> DeleteGenreAsync(int id)
{
    var deleted = await _context.Genres
        .Where(g => g.Id == id)
        .ExecuteDeleteAsync();
    return deleted > 0;
}

public async Task<Genre?> GetGenreAsync(int id)
{
    return await _context.Genres.FindAsync(id);
}

public async Task<List<Genre>> GetGenresAsync()
{
    return await _context.Genres
        .AsNoTracking()
        .ToListAsync();
}

public async Task<int> GetGenresAmountAsync()
{
    return await _context.Genres.CountAsync();
}

public async Task<bool> UpdateGenreAsync(Genre genre)
{
    var updated = await _context.Genres
        .Where(g => g.Id == genre.Id)
        .ExecuteUpdateAsync(s => s
            .SetProperty(g => g.Name, genre.Name));
    return updated > 0;
}
}
