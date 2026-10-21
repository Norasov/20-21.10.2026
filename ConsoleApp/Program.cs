using System.Security.Cryptography;
using Domain.Entities;
using Infrastruture.Repositories;

// var repo = new BookRepository();

// while (true)
// {
//     System.Console.WriteLine("========================================");
//     System.Console.WriteLine(@"1. Show all books.
// 2. Show book by id.
// 3. Add new book.
// 4. Update book.
// 5.Delete book.
// 0.Exit.");

//     var a = Console.ReadLine();
//     switch (a)
//     {
//         case "1":
//             var books = repo.GetBooks();
//             break;
//         case "2":
//             System.Console.Write("Enter id: ");
//             var id = Convert.ToInt32(Console.ReadLine());
//             var bb = repo.GetBook(id);
//             System.Console.WriteLine(bb.Id + " " + bb.Title + " " + bb.PublishedYear + " " + bb.TotalCopies);
//             break;
//         case "3":
//             System.Console.Write("Enter title: ");
//             var t = Console.ReadLine()!;
//             System.Console.Write("Enter published year: ");
//             var py = Convert.ToInt32(Console.ReadLine());
//             System.Console.Write("Enter total copies: ");
//             var tc = Convert.ToInt32(Console.ReadLine());
//             var bbb = new Book()
//             {
//                 Title = t,
//                 PublishedYear = py,
//                 TotalCopies = tc
//             };
//             repo.AddBook(bbb);
//             break;
//         case "4":
//             System.Console.Write("Enter id: ");
//             var uid = Convert.ToInt32(Console.ReadLine());
//             System.Console.Write("Enter title: ");
//             var ut = Console.ReadLine()!;
//             System.Console.Write("Enter published year: ");
//             var upy = Convert.ToInt32(Console.ReadLine());
//             System.Console.Write("Enter total copies: ");
//             var utc = Convert.ToInt32(Console.ReadLine());
//             var ubbb = new Book()
//             {
//                 Id = uid,
//                 Title = ut,
//                 PublishedYear = upy,
//                 TotalCopies = utc
//             };
//             repo.UpdateBook(ubbb);
//             break;
//         case "5":
//             System.Console.Write("Enter id: ");
//             var did = Convert.ToInt32(Console.ReadLine());
//             repo.DeleteBook(did);
//             break;
//         case "0":
//             System.Console.WriteLine("Exiting ...");
//             return;
//     }
// }


var author = new AuthorRepository();
while (true)
{
    System.Console.WriteLine("--------------------------------------------");
    System.Console.WriteLine(@"
1.Add new author
2.Get an author.
3.Get authors
4.Update author
5.Delete author
0.exit");
    var b = Console.ReadLine();
    switch (b)
    {

    case "1":
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
    case "2":
        System.Console.Write("Enter id: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var a = author.GetAuthor(id);
        System.Console.WriteLine(a.Id+" "+a.FullName+" "+a.Country+" "+a.BirthYear);
        break;
    case "3":
        var authors = author.GetAuthors();
        break;
    case "4":
        System.Console.Write("Enter id: ");
        var uid = Convert.ToInt32(Console.ReadLine());

        System.Console.Write("Enter name: ");
        var uname = Console.ReadLine()!;

        System.Console.Write("Enter country: ");
        var ucountry = Console.ReadLine()!;

        System.Console.Write("Enter birthyear: ");
        var ubirthyear = Convert.ToInt32(Console.ReadLine());
        Author uauthor = new()
        {
            Id = uid,
            FullName = uname,
            Country = ucountry,
            BirthYear = ubirthyear
        };
        author.UpdateAuthor(uauthor);
        break;
    case "5":
        System.Console.Write("Enter id: ");
        var id1 = Convert.ToInt32(Console.ReadLine());
        author.DeleteAuthor(id1);
        break;
    case "0":
        System.Console.WriteLine("Program is closed");
        return;
    }
}