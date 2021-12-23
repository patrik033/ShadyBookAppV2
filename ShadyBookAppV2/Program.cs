using ShadyBookAppV2;
using ShadyBookAppV2.Models;

UpdateAuthorsWithNewBooks(3);
Console.WriteLine("Adding completed!");
Console.ReadLine();


//Genres
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

//Authors
void AddAuthors()
{
    using (var context = new ShadyBookAppContext())
    {
        var authors = new List<Author>()
        {
            new Author()
            {
                FirstName = "Mikael",
                LastName = "Nilsson",
                BirthDate = new DateTime(1990,03,11)
            },
            new Author()
            {
                FirstName = "Claes",
                LastName = "Engelin",
                BirthDate= new DateTime(1956,04,23)
            },
            new Author()
            {
                FirstName = "Patrik",
                LastName = "Beijar Odh"
            }
        };
        context.Authors.AddRange(authors);
        context.SaveChanges();
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

//Stores
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


void UpdateAuthorsWithNewBooks(int id)
{
    using (var context = new ShadyBookAppContext())
    {
        var author = context.Authors.Find(id);
        if (author != null)
        {
            author.Books.Add(new Book() { Title = "Robins Finska Karameller", Price = 24, GenreId = 1 });
            context.SaveChanges();
        }
        else
            Console.WriteLine("Det finns ingen författare med det id:t");
    }
}
