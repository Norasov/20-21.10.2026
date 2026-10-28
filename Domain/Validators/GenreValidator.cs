using Domain.Entities;

namespace Domain.Validators;

public static class GenreValidator
{
    public static List<string> Validate(Genre genre)
    {
        List<string> errors = [];
        
        if(string.IsNullOrWhiteSpace(genre.Name))
            errors.Add("Название жанра не может быть пустым.");
        
        else if(genre.Name.Length > 100)
            errors.Add("Название жанра не должно быть длиннее 100 символов.");

        return errors;
    }
}
