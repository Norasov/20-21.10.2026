using Domain.Entities;
using Domain.Interfaces;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var delete = await _context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return delete > 0;
    }

    public async Task<int> GetAmountByGenreAsync(int genreId)
    {

        return await _context.Books.CountAsync(b => b.GenreId == genreId);
    }

    public async Task<int> GetAmountByPublishedYearAsync(int year)
    {
        return await _context.Books.CountAsync(b => b.PublishedYear == year);
    }


    public async Task<List<Book>> GetBooksAsync()
    {
        return await _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<int> GetBooksAmountAsync()
    {
        return await _context.Books.CountAsync();
    }


    public async Task<List<Book>> GetBooksByAuthorAsync(string fullname)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.Author.FullName.ToLower() == fullname.ToLower())
            .ToListAsync();
    }

    public async Task<List<Book>> GetBooksByGenreAsync(string name)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(g => g.Genre.Name.ToLower() == name.ToLower())
            .ToListAsync();
    }

    public async Task<int> GetMaxPublishedYearByAuthorAsync(int authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .MaxAsync(b => (int?)b.PublishedYear) ?? 0;
    }

    public async Task<int> GetMinPublishedYearByAuthorAsync(int authorId)
    {
        return await _context.Books
            .Where(b => b.AuthorId == authorId)
            .MinAsync(b => (int?)b.PublishedYear) ?? 0;
    }

    public async Task<int> GetMaxPublishedYearByGenreAsync(int genreId)
    {
        return await _context.Books
            .Where(b => b.GenreId == genreId)
            .MaxAsync(b => (int?)b.PublishedYear) ?? 0;
    }

    public async Task<int> GetMinPublishedYearByGenreAsync(int genreId)
    {
        return await _context.Books
            .Where(b => b.GenreId == genreId)
            .MinAsync(b => (int?)b.PublishedYear) ?? 0;
    }

    public async Task<bool> UpdateBookAsync(Book book)
    {
        var updated = await _context.Books
            .Where(b => b.Id == book.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, book.Title)
                .SetProperty(b => b.PublishedYear, book.PublishedYear)
                .SetProperty(b => b.TotalCopies, book.TotalCopies)
                .SetProperty(b => b.AuthorId, book.AuthorId)
                .SetProperty(b => b.GenreId, book.GenreId));
        return updated > 0;
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }
}
