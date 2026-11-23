using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;
    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddAuthorAsync(Author author)
    {
        _context.Authors.Add(author);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var hasBooks = await _context.Books.AnyAsync(b => b.AuthorId == id);
        if (hasBooks) return false;

        var delete = await _context.Authors
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return delete > 0;
    }

    public async Task<Author?> GetAuthorAsync(int id)
    {
        return await _context.Authors.FindAsync(id);
    }

    public async Task<List<Author>> GetAuthorsAsync()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<int> GetAuthorsAmountAsync()
    {
        return await _context.Authors.CountAsync();
    }

    public async Task<bool> UpdateAuthorAsync(Author author)
    {
        var update = _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        return true;
    }
}
