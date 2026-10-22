using System.Security.Cryptography;
using Domain.Entities;
using Infrastruture.Repositories;

var repo = new BookRepository();
var author = new AuthorRepository();
var genre = new GenreRepository();

while (true)
{
    System.Console.WriteLine("========================================");
    System.Console.WriteLine(@"1. Show all books.
2. Show book by id.
3. Add new book.
4. Update book.
5.Delete book.
6.Add new author
7.Get an author.
8.Get authors
9.Update author
10.Delete author
11.Add new genre
12.Get a genre.
13.Get genres
14.Update genre
15.Delete genre
16.Get books by author
17.Get books by genre
0.Exit.");

    var a = Console.ReadLine();
    switch (a)
    {
        case "1":
            var books = repo.GetBooks();
            break;
        case "2":
            System.Console.Write("Enter id: ");
            var id = Convert.ToInt32(Console.ReadLine());
            var bb = repo.GetBook(id);
            System.Console.WriteLine(bb.Id + " " + bb.Title + " " + bb.PublishedYear + " " + bb.TotalCopies);
            break;
        case "3":
            System.Console.Write("Enter title: ");
            var t = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var py = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var tc = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter author id: ");
            var authorid = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter id of genre: ");
            var genreid = Convert.ToInt32(Console.ReadLine());
            var bbb = new Book()
            {
                Title = t,
                PublishedYear = py,
                TotalCopies = tc,
                AuthorId = authorid,
                GenreId = genreid
            };
            repo.AddBook(bbb);
            break;
        case "4":
            System.Console.Write("Enter id: ");
            var uid = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter title: ");
            var ut = Console.ReadLine()!;
            System.Console.Write("Enter published year: ");
            var upy = Convert.ToInt32(Console.ReadLine());
            System.Console.Write("Enter total copies: ");
            var utc = Convert.ToInt32(Console.ReadLine());
            var ubbb = new Book()
            {
                Id = uid,
                Title = ut,
                PublishedYear = upy,
                TotalCopies = utc
            };
            repo.UpdateBook(ubbb);
            break;
        case "5":
            System.Console.Write("Enter id: ");
            var did = Convert.ToInt32(Console.ReadLine());
            repo.DeleteBook(did);
            break;
        case "6":
            System.Console.Write("Enter name: ");
            var name = Console.ReadLine()!;

            System.Console.Write("Enter country: ");
            var country = Console.ReadLine()!;

            System.Console.Write("Enter birthyear: ");
            int birthyear = Convert.ToInt32(Console.ReadLine());
            Author author1 = new()
            {
                FullName = name,
                Country = country,
                BirthYear = birthyear
            };
            author.AddAuthor(author1);
            break;
        case "7":
            System.Console.Write("Enter id: ");
            var aid = Convert.ToInt32(Console.ReadLine());
            var aa = author.GetAuthor(aid);
            System.Console.WriteLine(aa.Id + " " + aa.FullName + " " + aa.Country + " " + aa.BirthYear);
            break;
        case "8":
            var authors = author.GetAuthors();
            break;
        case "9":
            System.Console.Write("Enter id: ");
            var auid = Convert.ToInt32(Console.ReadLine());

            System.Console.Write("Enter name: ");
            var uname = Console.ReadLine()!;

            System.Console.Write("Enter country: ");
            var ucountry = Console.ReadLine()!;

            System.Console.Write("Enter birthyear: ");
            var ubirthyear = Convert.ToInt32(Console.ReadLine());
            Author uauthor = new()
            {
                Id = auid,
                FullName = uname,
                Country = ucountry,
                BirthYear = ubirthyear
            };
            author.UpdateAuthor(uauthor);
            break;
        case "10":
            System.Console.Write("Enter id: ");
            var id1 = Convert.ToInt32(Console.ReadLine());
            author.DeleteAuthor(id1);
            break;
        case "11":
            System.Console.Write("Enter name: ");
            var gname = Console.ReadLine()!;

            Genre genre1 = new()
            {
                Name = gname
            };
            genre.AddGenre(genre1);
            break;
        case "12":
            System.Console.Write("Enter id: ");
            var gid = Convert.ToInt32(Console.ReadLine());
            var gg = genre.GetGenre(gid);
            System.Console.WriteLine(gg.Id + " " + gg.Name);
            break;
        case "13":
            var genres = genre.GetGenres();
            break;
        case "14":
            System.Console.Write("Enter id: ");
            var guid = Convert.ToInt32(Console.ReadLine());

            System.Console.Write("Enter name: ");
            var guname = Console.ReadLine()!;

            Genre ugenre = new()
            {
               Id = guid,
               Name = guname
            };
            genre.UpdateGenre(ugenre);
            break;
        case "15":
            System.Console.Write("Enter id: ");
            var gid1 = Convert.ToInt32(Console.ReadLine());
            genre.DeleteGenre(gid1);
            break;
        case "16":
            System.Console.Write("Enter name of author: ");
            var authorName = Console.ReadLine()!;
            repo.GetBooksByAuthor(authorName);
            break;
        case "17":
            System.Console.Write("Enter name of genre: ");
            var genreName = Console.ReadLine()!;
            repo.GetBooksByGenre(genreName);
            break;
        case "0":
            System.Console.WriteLine("Exiting ...");
            return;
    }
}

