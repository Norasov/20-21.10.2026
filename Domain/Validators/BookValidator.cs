using Domain.Entities;

namespace Domain.Validators;

public static class BookValidator
{
    public static List<string> Validate(Book book)
    {
        List<string> errors = [];

        if(string.IsNullOrWhiteSpace(book.Title))
            errors.Add("Название книги не должно быть пустым.");

        if(book.PublishedYear <= 0 || book.PublishedYear > DateTime.Now.Year)
            errors.Add($"Год выпуска должен быть от 1 до {DateTime.Now.Year}.");

        if(book.TotalCopies < 0)
            errors.Add("Количество копий не может быть отрицательным.");

        return errors;
    }
}
