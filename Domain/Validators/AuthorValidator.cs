using Domain.Entities;

namespace Domain.Validators;

public static class AuthorValidator
{
    public static List<string> Validate(Author author)
    {
        var errors = new List<string>();

        if(string.IsNullOrWhiteSpace(author.FullName))
            errors.Add("Имя автора не должно быть пустым.");

        if(string.IsNullOrWhiteSpace(author.Country))
            errors.Add("Страна не должна быть пустой.");

        if(author.BirthYear <= 0 || author.BirthYear > DateTime.Now.Year)
            errors.Add($"Год рождения должен быть от 1 до {DateTime.Now.Year}.");
        
        return errors;
    }
}
