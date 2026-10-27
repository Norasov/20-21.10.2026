using ConsoleApp;
using Domain.Entities;
using Domain.Validators;
using Infrastruture.Data;
using Infrastruture.Repositories;

Console.OutputEncoding = System.Text.Encoding.UTF8;

if (!Database.TestConnection(out var connectionError))
{
    Console.WriteLine($"Не удалось подключиться к базе данных: {connectionError}");
    return;
}

var books = new BookRepository();
var authors = new AuthorRepository();
var genres = new GenreRepository();

while (true)
{
    Console.WriteLine("========================================");
    Console.WriteLine(@"1. Все книги
2. Книга по id
3. Добавить книгу
4. Изменить книгу
5. Удалить книгу
6. Добавить автора
7. Автор по id
8. Все авторы
9. Изменить автора
10. Удалить автора
11. Добавить жанр
12. Жанр по id
13. Все жанры
14. Изменить жанр
15. Удалить жанр
16. Книги автора
17. Книги жанра
0. Выход");

    var choice = Input.ReadText("Выберите пункт: ");

    try
    {
        switch (choice)
        {
            case "1":
            {
                var all = books.GetBooks();
                if (all.Count == 0)
                {
                    Console.WriteLine("Книг нет.");
                    break;
                }

                foreach (var item in all)
                    Console.WriteLine($"{item.Id} | {item.Title} | {item.PublishedYear} | {item.TotalCopies}");
                break;
            }
            case "2":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                var book = books.GetBook(id.Value);
                if (book is null)
                {
                    Console.WriteLine("Книга не найдена.");
                    break;
                }

                Console.WriteLine($"{book.Id} | {book.Title} | {book.PublishedYear} | {book.TotalCopies}");
                break;
            }
            case "3":
            {
                var title = Input.ReadText("Введите название: ");

                var year = Input.ReadInt("Введите год выпуска: ");
                if (year is null) break;

                var copies = Input.ReadInt("Введите количество копий: ");
                if (copies is null) break;

                var bookAuthorId = Input.ReadInt("Введите id автора: ");
                if (bookAuthorId is null) break;

                var bookGenreId = Input.ReadInt("Введите id жанра: ");
                if (bookGenreId is null) break;

                var newBook = new Book
                {
                    Title = title,
                    PublishedYear = year.Value,
                    TotalCopies = copies.Value,
                    AuthorId = bookAuthorId.Value,
                    GenreId = bookGenreId.Value
                };

                if (Validation.HasErrors(BookValidator.Validate(newBook))) break;

                books.AddBook(newBook);
                Console.WriteLine("Книга добавлена.");
                break;
            }
            case "4":
            {
                var id = Input.ReadInt("Введите id книги: ");
                if (id is null) break;

                var existing = books.GetBook(id.Value);
                if (existing is null)
                {
                    Console.WriteLine("Книга не найдена.");
                    break;
                }

                var title = Input.ReadText("Введите название: ");

                var year = Input.ReadInt("Введите год выпуска: ");
                if (year is null) break;

                var copies = Input.ReadInt("Введите количество копий: ");
                if (copies is null) break;

                var bookAuthorId = Input.ReadInt("Введите id автора: ");
                if (bookAuthorId is null) break;

                var bookGenreId = Input.ReadInt("Введите id жанра: ");
                if (bookGenreId is null) break;

                existing.Title = title;
                existing.PublishedYear = year.Value;
                existing.TotalCopies = copies.Value;
                existing.AuthorId = bookAuthorId.Value;
                existing.GenreId = bookGenreId.Value;

                if (Validation.HasErrors(BookValidator.Validate(existing))) break;

                books.UpdateBook(existing);
                Console.WriteLine("Книга обновлена.");
                break;
            }
            case "5":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                if (books.GetBook(id.Value) is null)
                {
                    Console.WriteLine("Книга не найдена.");
                    break;
                }

                books.DeleteBook(id.Value);
                Console.WriteLine("Книга удалена.");
                break;
            }
            case "6":
            {
                var name = Input.ReadText("Введите имя: ");
                var country = Input.ReadText("Введите страну: ");

                var birthYear = Input.ReadInt("Введите год рождения: ");
                if (birthYear is null) break;

                var newAuthor = new Author
                {
                    FullName = name,
                    Country = country,
                    BirthYear = birthYear.Value
                };

                if (Validation.HasErrors(AuthorValidator.Validate(newAuthor))) break;

                authors.AddAuthor(newAuthor);
                Console.WriteLine("Автор добавлен.");
                break;
            }
            case "7":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                var found = authors.GetAuthor(id.Value);
                if (found is null)
                {
                    Console.WriteLine("Автор не найден.");
                    break;
                }

                Console.WriteLine($"{found.Id} | {found.FullName} | {found.Country} | {found.BirthYear}");
                break;
            }
            case "8":
            {
                var all = authors.GetAuthors();
                if (all.Count == 0)
                {
                    Console.WriteLine("Авторов нет.");
                    break;
                }

                foreach (var item in all)
                    Console.WriteLine($"{item.Id} | {item.FullName} | {item.Country} | {item.BirthYear}");
                break;
            }
            case "9":
            {
                var id = Input.ReadInt("Введите id автора: ");
                if (id is null) break;

                var existing = authors.GetAuthor(id.Value);
                if (existing is null)
                {
                    Console.WriteLine("Автор не найден.");
                    break;
                }

                var name = Input.ReadText("Введите имя: ");
                var country = Input.ReadText("Введите страну: ");

                var birthYear = Input.ReadInt("Введите год рождения: ");
                if (birthYear is null) break;

                existing.FullName = name;
                existing.Country = country;
                existing.BirthYear = birthYear.Value;

                if (Validation.HasErrors(AuthorValidator.Validate(existing))) break;

                authors.UpdateAuthor(existing);
                Console.WriteLine("Автор обновлён.");
                break;
            }
            case "10":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                if (authors.GetAuthor(id.Value) is null)
                {
                    Console.WriteLine("Автор не найден.");
                    break;
                }

                authors.DeleteAuthor(id.Value);
                Console.WriteLine("Автор удалён.");
                break;
            }
            case "11":
            {
                var name = Input.ReadText("Введите название жанра: ");
                var newGenre = new Genre { Name = name };

                if (Validation.HasErrors(GenreValidator.Validate(newGenre))) break;

                genres.AddGenre(newGenre);
                Console.WriteLine("Жанр добавлен.");
                break;
            }
            case "12":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                var found = genres.GetGenre(id.Value);
                if (found is null)
                {
                    Console.WriteLine("Жанр не найден.");
                    break;
                }

                Console.WriteLine($"{found.Id} | {found.Name}");
                break;
            }
            case "13":
            {
                var all = genres.GetGenres();
                if (all.Count == 0)
                {
                    Console.WriteLine("Жанров нет.");
                    break;
                }

                foreach (var item in all)
                    Console.WriteLine($"{item.Id} | {item.Name}");
                break;
            }
            case "14":
            {
                var id = Input.ReadInt("Введите id жанра: ");
                if (id is null) break;

                var existing = genres.GetGenre(id.Value);
                if (existing is null)
                {
                    Console.WriteLine("Жанр не найден.");
                    break;
                }

                existing.Name = Input.ReadText("Введите новое название: ");

                if (Validation.HasErrors(GenreValidator.Validate(existing))) break;

                genres.UpdateGenre(existing);
                Console.WriteLine("Жанр обновлён.");
                break;
            }
            case "15":
            {
                var id = Input.ReadInt("Введите id: ");
                if (id is null) break;

                if (genres.GetGenre(id.Value) is null)
                {
                    Console.WriteLine("Жанр не найден.");
                    break;
                }

                genres.DeleteGenre(id.Value);
                Console.WriteLine("Жанр удалён.");
                break;
            }
            case "16":
            {
                var authorName = Input.ReadText("Введите имя автора: ");
                var found = books.GetBooksByAuthor(authorName);
                if (found.Count == 0)
                {
                    Console.WriteLine("Ничего не найдено.");
                    break;
                }

                foreach (var item in found)
                    Console.WriteLine($"{item.Id} | {item.Title} | {item.PublishedYear} | {item.TotalCopies}");
                break;
            }
            case "17":
            {
                var genreName = Input.ReadText("Введите название жанра: ");
                var found = books.GetBooksByGenre(genreName);
                if (found.Count == 0)
                {
                    Console.WriteLine("Ничего не найдено.");
                    break;
                }

                foreach (var item in found)
                    Console.WriteLine($"{item.Id} | {item.Title} | {item.PublishedYear} | {item.TotalCopies}");
                break;
            }
            case "0":
                Console.WriteLine("Выход...");
                return;
            default:
                Console.WriteLine("Неизвестный пункт меню.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(DbErrors.Describe(ex));
    }
}