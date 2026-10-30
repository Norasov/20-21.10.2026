using ConsoleApp;
using Domain.Entities;
using Domain.Validators;
using Infrastruture.Repositories;
using Infrastruture.Data;

Console.OutputEncoding = System.Text.Encoding.UTF8;
DapperContext context = new();
if (!context.TestConnection(out var connectionError))
{
    System.Console.WriteLine($"Не удалось подключиться к базе данных: {connectionError}");
    return;
}



var books = new BookRepository(context);
var authors = new AuthorRepository(context);
var genres = new GenreRepository(context);

while (true)
{
    Console.WriteLine("========================================");
    Console.WriteLine(@"1. Все книги
2. Книга по id
3. Добавить книгу
4. Изменить книгу
5. Удалить книгу
6. Книги автора
7. Книги жанра
8. общее Количество книг
9. Самый ранний год выпуска книги автора
10. Самый поздний год выпуска книги автора
11. Самый ранний год выпуска книги по жанру
12. Самый поздний год выпуска книги по жанру
13. Количество книг выпущенных в определенном году
14. Количество книг определенного жанра
15. Добавить автора
16. Автор по id
17. Все авторы
18. Изменить автора
19. Удалить автора
20. Общее количество авторов
21. Добавить жанр
22. Жанр по id
23. Все жанры
24. Изменить жанр
25. Удалить жанр
26. Общее количество жанров
0. Выход");

    var choice = Input.ReadText("Выберите пункт: ");

    try
    {
        switch (choice)
        {
            case "1":
                {
                    var all = await books.GetBooksAsync();
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

                    var book = await books.GetBookAsync(id.Value);
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
                    if(await authors.GetAuthorAsync(bookAuthorId.Value) is null)
                    {
                        System.Console.WriteLine("Автор не найден.");
                        break;
                    }

                    var bookGenreId = Input.ReadInt("Введите id жанра: ");
                    if (bookGenreId is null) break;
                    if(await genres.GetGenreAsync(bookGenreId.Value) is null)
                    {
                        System.Console.WriteLine("Жанр не найден.");
                        break;
                    }

                    var newBook = new Book
                    {
                        Title = title,
                        PublishedYear = year.Value,
                        TotalCopies = copies.Value,
                        AuthorId = bookAuthorId.Value,
                        GenreId = bookGenreId.Value
                    };

                    if (Validation.HasErrors(BookValidator.Validate(newBook))) break;

                    await books.AddBookAsync(newBook);
                    Console.WriteLine("Книга добавлена.");
                    break;
                }
            case "4":
                {
                    var id = Input.ReadInt("Введите id книги: ");
                    if (id is null) break;

                    var existing = await books.GetBookAsync(id.Value);
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
                    if(await authors.GetAuthorAsync(bookAuthorId.Value) is null)
                    {
                        System.Console.WriteLine("Автор не найден.");
                        break;
                    }

                    var bookGenreId = Input.ReadInt("Введите id жанра: ");
                    if (bookGenreId is null) break;
                    if(await genres.GetGenreAsync(bookGenreId.Value) is null)
                    {
                        System.Console.WriteLine("Жанр не найден.");
                        break;
                    }

                    existing.Title = title;
                    existing.PublishedYear = year.Value;
                    existing.TotalCopies = copies.Value;
                    existing.AuthorId = bookAuthorId.Value;
                    existing.GenreId = bookGenreId.Value;

                    if (Validation.HasErrors(BookValidator.Validate(existing))) break;

                    await books.UpdateBookAsync(existing);
                    Console.WriteLine("Книга обновлена.");
                    break;
                }
            case "5":
                {
                    var id = Input.ReadInt("Введите id: ");
                    if (id is null) break;

                    if (await books.GetBookAsync(id.Value) is null)
                    {
                        Console.WriteLine("Книга не найдена.");
                        break;
                    }

                    await books.DeleteBookAsync(id.Value);
                    Console.WriteLine("Книга удалена.");
                    break;
                }
            case "6":
                {
                    var authorName = Input.ReadText("Введите имя автора: ");
                    var found = await books.GetBooksByAuthorAsync(authorName);
                    if (found.Count == 0)
                    {
                        Console.WriteLine("Ничего не найдено.");
                        break;
                    }

                    foreach (var item in found)
                        Console.WriteLine($"{item.Id} | {item.Title} | {item.PublishedYear} | {item.TotalCopies}");
                    break;
                }
            case "7":
                {
                    var genreName = Input.ReadText("Введите название жанра: ");
                    var found = await books.GetBooksByGenreAsync(genreName);
                    if (found.Count == 0)
                    {
                        Console.WriteLine("Ничего не найдено.");
                        break;
                    }

                    foreach (var item in found)
                        Console.WriteLine($"{item.Id} | {item.Title} | {item.PublishedYear} | {item.TotalCopies}");
                    break;
                }
            case "8":
                {
                    System.Console.WriteLine($"Книг: {await books.GetBooksAmountAsync()}");
                    break;
                }
            case "9":
                {
                    var id = Input.ReadInt("Введите id автора: ");
                    if (id is null) break;
                    if (await authors.GetAuthorAsync(id.Value) is null)
                    {
                        System.Console.WriteLine("Автор не найден.");
                        break;
                    }
                    var minYear = await books.GetMinPublishedYearByAuthorAsync(id.Value);
                    if (minYear == 0)
                    {
                        System.Console.WriteLine("У автора нет книг.");
                        break;
                    }
                    System.Console.WriteLine($"Самый ранний год выпуска: {minYear}");
                    break;
                }
            case "10":
                {
                    var id = Input.ReadInt("Введите id автора: ");
                    if (id is null) break;
                    if (await authors.GetAuthorAsync(id.Value) is null)
                    {
                        System.Console.WriteLine("Автор не найден.");
                        break;
                    }
                    var maxYear = await books.GetMaxPublishedYearByAuthorAsync(id.Value);
                    if (maxYear == 0)
                    {
                        System.Console.WriteLine("У автора нет книг.");
                        break;
                    }
                    System.Console.WriteLine($"Самый поздний год выпуска: {maxYear}");
                    break;
                }
            case "11":
                {
                    var id = Input.ReadInt("Введите id жaнра: ");
                    if (id is null) break;
                    if (await genres.GetGenreAsync(id.Value) is null)
                    {
                        System.Console.WriteLine("Жанр не найден.");
                        break;
                    }
                    var minYear = await books.GetMinPublishedYearByGenreAsync(id.Value);
                    if (minYear == 0)
                    {
                        System.Console.WriteLine("Нет книг с таким жанром");
                        break;
                    }
                    System.Console.WriteLine($"Самый ранний год выпуска книги этого жанра: {minYear}");
                    break;
                }
            case "12":
                {
                    var id = Input.ReadInt("Введите id жaнра: ");
                    if (id is null) break;
                    if (await genres.GetGenreAsync(id.Value) is null)
                    {
                        System.Console.WriteLine("Жанр не найден.");
                        break;
                    }
                    var maxYear = await books.GetMaxPublishedYearByGenreAsync(id.Value);
                    if (maxYear == 0)
                    {
                        System.Console.WriteLine("Нет книг с таким жанром");
                        break;
                    }
                    System.Console.WriteLine($"Самый поздний год выпуска книги этого жанра: {maxYear}");
                    break;
                }
            case "13":
                {
                    var year = Input.ReadInt("Введите год выпуска: ");
                    if(year is null) break;
                    if(year <= 0 || year > DateTime.Now.Year)
                    {
                        System.Console.WriteLine("Введите корректный год");
                        break;
                    }
                    var countBooksOfYear = await books.GetAmountByPublishedYearAsync(year.Value);
                    if(countBooksOfYear == 0)
                    {
                        System.Console.WriteLine("В этом году книг не выпущено.");
                        break;
                    }
                    System.Console.WriteLine($"Количество книг этого года: {countBooksOfYear}");
                    break;
                }
            case "14":
                {
                    var id = Input.ReadInt("Введите id жанра: ");
                    if(id is null) break;
                    if(await genres.GetGenreAsync(id.Value) is null)
                    {
                        System.Console.WriteLine("Жанр не найден.");
                        break;
                    }
                    var countBooksOfGenre = await books.GetAmountByGenreAsync(id.Value);
                    if(countBooksOfGenre == 0)
                    {
                        System.Console.WriteLine("Нет книг с таким жанром");
                        break;
                    }
                    System.Console.WriteLine($"Количество книг с таким жанром: {countBooksOfGenre}");
                    break;
                }
            case "15":
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

                    await authors.AddAuthorAsync(newAuthor);
                    Console.WriteLine("Автор добавлен.");
                    break;
                }
            case "16":
                {
                    var id = Input.ReadInt("Введите id: ");
                    if (id is null) break;

                    var found = await authors.GetAuthorAsync(id.Value);
                    if (found is null)
                    {
                        Console.WriteLine("Автор не найден.");
                        break;
                    }

                    Console.WriteLine($"{found.Id} | {found.FullName} | {found.Country} | {found.BirthYear}");
                    break;
                }
            case "17":
                {
                    var all = await authors.GetAuthorsAsync();
                    if (all.Count == 0)
                    {
                        Console.WriteLine("Авторов нет.");
                        break;
                    }

                    foreach (var item in all)
                        Console.WriteLine($"{item.Id} | {item.FullName} | {item.Country} | {item.BirthYear}");
                    break;
                }
            case "18":
                {
                    var id = Input.ReadInt("Введите id автора: ");
                    if (id is null) break;

                    var existing = await authors.GetAuthorAsync(id.Value);
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

                    await authors.UpdateAuthorAsync(existing);
                    Console.WriteLine("Автор обновлён.");
                    break;
                }
            case "19":
                {
                    var id = Input.ReadInt("Введите id: ");
                    if (id is null) break;

                    if (await authors.GetAuthorAsync(id.Value) is null)
                    {
                        Console.WriteLine("Автор не найден.");
                        break;
                    }

                    await authors.DeleteAuthorAsync(id.Value);
                    Console.WriteLine("Автор удалён.");
                    break;
                }
            case "20":
                {
                    System.Console.WriteLine($"Авторов: {await authors.GetAuthorsAmountAsync()}");
                    break;
                }
            case "21":
                {
                    var name = Input.ReadText("Введите название жанра: ");
                    var newGenre = new Genre { Name = name };

                    if (Validation.HasErrors(GenreValidator.Validate(newGenre))) break;

                    await genres.AddGenreAsync(newGenre);
                    Console.WriteLine("Жанр добавлен.");
                    break;
                }
            case "22":
                {
                    var id = Input.ReadInt("Введите id: ");
                    if (id is null) break;

                    var found = await genres.GetGenreAsync(id.Value);
                    if (found is null)
                    {
                        Console.WriteLine("Жанр не найден.");
                        break;
                    }

                    Console.WriteLine($"{found.Id} | {found.Name}");
                    break;
                }
            case "23":
                {
                    var all = await genres.GetGenresAsync();
                    if (all.Count == 0)
                    {
                        Console.WriteLine("Жанров нет.");
                        break;
                    }

                    foreach (var item in all)
                        Console.WriteLine($"{item.Id} | {item.Name}");
                    break;
                }
            case "24":
                {
                    var id = Input.ReadInt("Введите id жанра: ");
                    if (id is null) break;

                    var existing = await genres.GetGenreAsync(id.Value);
                    if (existing is null)
                    {
                        Console.WriteLine("Жанр не найден.");
                        break;
                    }

                    existing.Name = Input.ReadText("Введите новое название: ");

                    if (Validation.HasErrors(GenreValidator.Validate(existing))) break;

                    await genres.UpdateGenreAsync(existing);
                    Console.WriteLine("Жанр обновлён.");
                    break;
                }
            case "25":
                {
                    var id = Input.ReadInt("Введите id: ");
                    if (id is null) break;

                    if (await genres.GetGenreAsync(id.Value) is null)
                    {
                        Console.WriteLine("Жанр не найден.");
                        break;
                    }

                    await genres.DeleteGenreAsync(id.Value);
                    Console.WriteLine("Жанр удалён.");
                    break;
                }
            case "26":
                {
                    System.Console.WriteLine($"Жанров: {await genres.GetGenresAmountAsync()}");
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