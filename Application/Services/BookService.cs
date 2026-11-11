using Application.DTOs.Books;
using Application.HttpStatusCodes;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;
    public async Task<ServiceStatusCodes<bool>> AddBookAsync(CreateBookDTO bookDTO)
    {
        Book book = new()
        {
            Title = bookDTO.Title,
            PublishedYear = bookDTO.PublishedYear,
            TotalCopies = bookDTO.TotalCopies,
            AuthorId = bookDTO.AuthorId,
            GenreId = bookDTO.GenreId
        };
        var add = await _repository.AddBookAsync(book);

        return ServiceStatusCodes<bool>.Created(add);
    }

    public async Task<ServiceStatusCodes<bool>> DeleteBookAsync(int id)
    {
        var delete = await _repository.DeleteBookAsync(id);
        if(!delete)
            return ServiceStatusCodes<bool>.NotFound("Нет книги с таким идентификатором.");

        return ServiceStatusCodes<bool>.NoContent();
    }

    public async Task<ServiceStatusCodes<BookDTO>> GetBookAsync(int id)
    {
        var book = await _repository.GetBookAsync(id);
        if(book is null)
            return ServiceStatusCodes<BookDTO>.NotFound("Нет книги с ткаим идентификатором.");
        
        BookDTO bookDTO = new()
        {
            Title = book.Title,
            PublishedYear = book.PublishedYear,
            TotalCopies = book.TotalCopies,
        };

        return ServiceStatusCodes<BookDTO>.Ok(bookDTO);
    }

    public async Task<ServiceStatusCodes<int>> GetBooksAmountAsync()
    {
        var amount = await _repository.GetBooksAmountAsync();
        return ServiceStatusCodes<int>.Ok(amount);
    }


    public async Task<ServiceStatusCodes<List<BookDTO>>> GetBooksAsync()
    {
        var books = await _repository.GetBooksAsync();

        List<BookDTO> bookDTOs = [];
        foreach (var book in books)
        {
            BookDTO bookDTO = new()
            {
                Title = book.Title,
                PublishedYear = book.PublishedYear,
                TotalCopies = book.TotalCopies
            };
            bookDTOs.Add(bookDTO);
        }

        return ServiceStatusCodes<List<BookDTO>>.Ok(bookDTOs);
    }

    public async Task<ServiceStatusCodes<bool>> UpdateBookAsync(UpdateBookDTO bookDTO, int id)
    {
        var update = await _repository.GetBookAsync(id);
        if(update is null)
            return ServiceStatusCodes<bool>.NotFound("Нет книги с таким идентификатором.");
        

        update.Title = bookDTO.Title;
        update.PublishedYear = bookDTO.PublishedYear;
        update.AuthorId = bookDTO.AuthorId;
        update.GenreId = bookDTO.GenreId;

        var updated = await _repository.UpdateBookAsync(update);

        return ServiceStatusCodes<bool>.Ok(updated);
    }

}
