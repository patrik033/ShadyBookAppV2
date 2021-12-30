using Microsoft.EntityFrameworkCore;
using ShadyBookAppV2;
using ShadyBookAppV2.Models;

UpdateAuthor();
//JoinAuthorAndBooks();
//ListAllBooks();
//ListAllAuthors();
//DeleteAuthorWithBooks();
//DeleteBook();
Console.WriteLine("Färdig");
Console.ReadLine();


#region Lists
void ListAllGenres()
{
    using (var context = new ShadyBookAppContext())
    {
        var genres = context.Genres
            .ToList();

        foreach (var item in genres)
        {
            Console.WriteLine($"{item.Id} {item.GenreName}");
        }
    }
}
void ListAllAuthors()
{
    using (var context = new ShadyBookAppContext())
    {
        var allAuthors = context.Authors.ToList();
        foreach (var item in allAuthors)
        {
            Console.WriteLine($"{item.Id} {item.FirstName} {item.LastName} {item.BirthDate}");
        }
    }
}
void ListAllBooks()
{
    using (var context = new ShadyBookAppContext())
    {
        var books = context.Books.ToList();

        foreach (var item in books)
        {
            Console.WriteLine($"Id: {item.Id}, Title: {item.Title}, AuthorsId: {item.AuthorsId} ");
        }
    }
}
void ListAllStores()
{
    using (var context = new ShadyBookAppContext())
    {
        var store = context.Stores.ToList();
        foreach (var item in store)
        {
            Console.WriteLine($"{item.Id}: {item.StoreName}, {item.Address}");
        }
    }
}
#endregion

#region Add
void AddGenres()
{
    using (var context = new ShadyBookAppContext())
    {
        //var genres = context.Genres;
        var genre = new List<Genre>()
    {
        new Genre()
        {
            GenreName = "Skräck"
        },
        new Genre()
        {
            GenreName = "Fantasy"
        },
        new Genre()
        {
            GenreName = "Sci-fi"
        }
    };
        context.Genres.AddRange(genre);
        context.SaveChanges();
    }
}
void AddBook()
{
    using (var context = new ShadyBookAppContext())
    {
        var book = new Book()
        {
            AuthorsId = 1,
            Title = "Spaghetti Vol 1",
            Price = 12.45M,
            ReleaseDate = DateTime.Now,
            GenreId = 2
        };
        context.Books.Add(book);
        context.SaveChanges();
    }
}
void AddAll()
{
    using (var context = new ShadyBookAppContext())
    {
        Author a1 = new Author()
        {
            FirstName = "Test1",
            LastName = "Test2",

        };
        Book b1 = new Book()
        {
            Title = "some",
            Price = 12.34M,
            GenreId = 1
        };
        Book b2 = new Book()
        {
            Title = "some2",
            Price = 42.34M,
            GenreId = 2
        };

        a1.Books = new List<Book> { b1, b2 };
        context.Add(a1);
        context.SaveChanges();
    }
}
void AddStores()
{
    using (var context = new ShadyBookAppContext())
    {
        var stores = new List<Store>()
        {
            new Store()
            {
                StoreName = "Classes Bibliotek",
                Address = "Stora vägen 123"
            },
            new Store()
            {
                StoreName = "Amazon",
                Address = "Lilla vägen 123"
            },
            new Store()
            {
                StoreName = "Ebay",
                Address = "Mellan vägen 321"
            }
        };
        context.Stores.AddRange(stores);
        context.SaveChanges();
    }
}
void AddToStore()
{
    ListAllBooks();
    Console.Write("Skriv in ett id: ");
    ulong id = ulong.Parse(Console.ReadLine());

    Console.WriteLine();

    ListAllStores();
    Console.Write("Vilken affär vill du fylla på?: ");
    int storeId = int.Parse(Console.ReadLine());

    Console.WriteLine();

    Console.WriteLine("Hur många böcker vill du lägga till: ");
    int quantity = int.Parse(Console.ReadLine());




    using (var context = new ShadyBookAppContext())
    {
        var s = context.Stocks.Where(x => x.StoreId == storeId && x.BookId == id).FirstOrDefault();
        if (s == null)
        {
            var st = new Stock
            {
                StoreId = storeId,
                BookId = id,
                StockItem = quantity
            };
            context.Add(st);
            context.SaveChanges();
        }
        else
        {
            s.StockItem += quantity;
            context.SaveChanges();
        }
    }
}

void RemoveFromStore()
{

    ListAllBooks();
    Console.Write("Type a Book ID: ");
    ulong id = ulong.Parse(Console.ReadLine());

    Console.WriteLine();

    ListAllStores();
    Console.Write("What Store do you wish to remove from (Store ID)?: ");
    int storeId = int.Parse(Console.ReadLine());






    using (var context = new ShadyBookAppContext())
    {
        var s = context.Stocks.Where(x => x.StoreId == storeId && x.BookId == id).FirstOrDefault();
        if (s != null)
        {

            context.Remove(s);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("The BookID och the StoreID or the combination does not exist or is already null");
        }
    }


}

#endregion

#region AddAuthors
DateTime ConvertToDateTime(string date)
{
    try
    {
        DateTime newDate = Convert.ToDateTime(date);
        return newDate;
    }
    catch
    {
        return Convert.ToDateTime("2000-01-01");
    }
}
void AddAuthorsUi()
{
    Console.Write("Förnamn: ");
    string fName = Console.ReadLine();

    Console.Write("EfterNamn: ");
    string lName = Console.ReadLine();

    Console.Write("Födelsedatum, ange XXXX-XX-XX: ");
    string date = Console.ReadLine();

    AddAuthors(fName, lName, date);
}
void AddAuthors(string firstName, string lastName, string date)
{
    using (var context = new ShadyBookAppContext())
    {
        var authors = new Author
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = ConvertToDateTime(date)
        };
        context.Authors.Add(authors);
        context.SaveChanges();
    }
}
#endregion

#region Updates
void UpdateAuthor()
{
    ListAllAuthors();
    Console.Write("Please enter the author you want to update: ");
    string isString = Console.ReadLine();
    bool isTrue = int.TryParse(isString, out int isInt);

    while (isTrue != true)
    {
        Console.Write("Please enter a valid number: ");
        isString = Console.ReadLine();
        isTrue = int.TryParse(isString, out isInt);
    }

    using (var context = new ShadyBookAppContext())
    {
        var author = context.Authors.Find(isInt);
        if (author != null)
        {
            string firstName = ReturnInput("Enter First Name: ");
            string lastName = ReturnInput("Enter Last Name: ");
            Console.Write("Please enter a date to update, please use the format 'YYYY-MM-DD'.\nIf you don't enter it a default value will be entered: ");
            string newDate = Console.ReadLine();
            DateTime updatedDate = ConvertToDateTime(newDate);


            author.BirthDate = updatedDate;
            author.FirstName = firstName;
            author.LastName = lastName;
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("You entered an invalid author");
        }
    }
}
void UpdateBook(ulong bookId, int authorsId, string title, decimal price, string newReleaseDate, int genreId)
{
    using (var context = new ShadyBookAppContext())
    {
        DateTime newRelease = Convert.ToDateTime(newReleaseDate);
        var au = context.Books.Where(x => x.Id == bookId).FirstOrDefault();
        if (au != null)
        {
            au.Title = title;
            au.Price = price;
            au.ReleaseDate = newRelease;
            au.GenreId = genreId;
            au.AuthorsId = authorsId;
        }
        context.SaveChanges();
    }
}
void UpdateAuthorsWithNewBooks()
{
    using (var context = new ShadyBookAppContext())
    {

        ListAllAuthors();
        Console.Write("Please enter an author to add a book to: ");
        int choice = int.Parse(Console.ReadLine());
        Console.Write("Please enter title: ");
        string title = Console.ReadLine();
        Console.Write("Please enter a price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        ListAllGenres();
        Console.Write("Please enter a genre: ");
        int genreId = int.Parse(Console.ReadLine());



        var author = context.Authors.Find(choice);
        if (author != null)
        {
            author.Books.Add(new Book() { Title = title, Price = price, GenreId = genreId, AuthorsId = choice });
            context.SaveChanges();
        }
        else
            Console.WriteLine("Det finns ingen författare med det id:t");
    }
}
#endregion

#region Deletes
//färdig
void DeleteAuthorWithBooks()
{
    ListAllAuthors();
    Console.Write("Choice which author you want to delete: ");
    string choiceAsString = Console.ReadLine();
    bool choice = int.TryParse(choiceAsString, out int choiceAsInt);
    while (choice != true)
    {
        Console.WriteLine("Please enter a number: ");
        choiceAsString = Console.ReadLine();
        choice = int.TryParse(choiceAsString, out choiceAsInt);
    }

    using (var context = new ShadyBookAppContext())
    {
        var author = context.Authors.Find(choiceAsInt);
        if (author != null)
        {
            var books = context.Books.Where(b => b.AuthorsId == choiceAsInt);
            foreach (var book in books)
            {
                if (author != null)
                {
                    context.Entry(book).State = EntityState.Deleted;
                }
            }
            context.Remove(author);
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Author not found");
        }
    }
}

//färdig


void DeleteBook()
{
    ListAllBooks();
    Console.Write("Enter which number you want to delete: ");
    string isString = Console.ReadLine();
    bool isCorrect = ulong.TryParse(isString, out ulong isUlong);

    while (isCorrect != true)
    {
        Console.WriteLine("Enter a correct number: ");
        isString = Console.ReadLine();
        isCorrect = ulong.TryParse(isString, out isUlong);
    }

    using (var db = new ShadyBookAppContext())
    {
        var book = db.Books.Find(isUlong);

        if (book != null)
        {
            db.Entry(book).State = EntityState.Deleted;
            db.SaveChanges();
        }
        else
        {
            Console.WriteLine("You enter a invalid isbn number");
        }
    }
}
#endregion




#region QueriesForJoinTables
void JoinAuthorAndBooks()
{
    using (var context = new ShadyBookAppContext())
    {

        var data2 = from a in context.Authors
                    join b in context.Books
                    on a.Id equals b.AuthorsId
                    group b by a.FirstName into a2
                    select new
                    {
                        Author = a2.Key,
                        Book = a2.Select(book => book)
                    };
        foreach (var item in data2)
        {
            Console.WriteLine(item.Author);
            foreach (var Book in item.Book)
            {
                Console.WriteLine($"    {Book.Title}");
            }
        }
        Console.WriteLine();
    }
}
void JoinStoreAndBookAndStock()
{
    using (var context = new ShadyBookAppContext())
    {
        var data = (from b in context.Books
                    join st in context.Stocks
                     on b.Id equals st.BookId
                    join stc in context.Stores
                    on st.StoreId equals stc.Id into left
                    from left2 in left.DefaultIfEmpty()
                    select new
                    {
                        StoreName = (left2 == null ? "null" : left2.StoreName),
                        StockAmount = st.StockItem,
                        BookName = b.Title
                    }).ToList();
        foreach (var item in data)
        {
            Console.WriteLine($"Store Name: {item.StoreName} Quantity: {item.StockAmount} Title: {item.BookName}");
        }
    }
}
#endregion


//Ladda in dummy data till databasen

//Unödig?
void JoinExistingBooksAndAuthors()
{
    using (var db = new ShadyBookAppContext())
    {
        var s = db.Books.FirstOrDefault();
        var c = db.Authors.FirstOrDefault();


        s.Authors.Add(c);
        db.SaveChanges();
    }
}


void StartUp()
{
    DataBaseFiles.AddStartupGenres();
    DataBaseFiles.AddStarterAuthorsAndBooks();
    DataBaseFiles.AddStarterStores();
    DataBaseFiles.AddStarterToStores();
}

string ReturnInput(string message)
{
    Console.Write($"{message}: ");
    string output = Console.ReadLine();
    return output;
}




