using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShadyBookAppV2.Models;

namespace ShadyBookAppV2
{
    public static class DataBaseFiles
    {

        public static void AddStartupGenres()
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
        },
        new Genre()
        {
            GenreName = "Drama"
        },
        new Genre()
        {
            GenreName = "Action"
        }
    };
                context.Genres.AddRange(genre);
                context.SaveChanges();
            }
        }


        public static void AddStarterAuthorsAndBooks()
        {
            using (var context = new ShadyBookAppContext())
            {

                {
                    Author a1 = new Author
                    {
                        FirstName = "Claes",
                        LastName = "Engelin",
                        BirthDate = new DateTime(1990, 08, 25)
                    };
                    Author a2 = new Author
                    {
                        FirstName = "Alfons",
                        LastName = "Åberg",
                        BirthDate = new DateTime(1988, 04, 15)
                    };
                    Author a3 = new Author
                    {
                        FirstName = "Lisa",
                        LastName = "Humbug",
                        BirthDate = new DateTime(1979, 09, 29)
                    };
                    Author a4 = new Author
                    {
                        FirstName = "Patrik",
                        LastName = "Beijerman",
                        BirthDate = new DateTime(1987, 04, 01)
                    };
                    Author a5 = new Author
                    {
                        FirstName = "Mikael",
                        LastName = "Brovsky",
                        BirthDate = new DateTime(1990, 03, 15)
                    };
                    Author a6 = new Author
                    {
                        FirstName = "Bruno",
                        LastName = "Lipsky",
                        BirthDate = new DateTime(1993, 10, 29)
                    };
                    Book b1 = new Book
                    {
                        Title = "Spagetti alá kod",
                        Price = 199.00M,
                        GenreId = 4,
                        AuthorsId =1,
                        ReleaseDate = new DateTime(2010, 05, 15)

                    };
                    Book b2 = new Book
                    {
                        Title = "Köttbullar med Claes",
                        Price = 299.00M,
                        GenreId = 1,
                        AuthorsId = 1,
                        ReleaseDate = new DateTime(2011, 06, 25)
                    };
                    Book b3 = new Book
                    {
                        Title = "Ananas Dansen",
                        Price = 99.00M,
                        GenreId = 5,
                        AuthorsId = 2,
                        ReleaseDate = new DateTime(2015, 09, 01)
                    };
                    Book b4 = new Book
                    {
                        Title = "Alien Invasion",
                        Price = 599.00M,
                        GenreId = 1,
                        AuthorsId = 2,
                        ReleaseDate = new DateTime(2020, 01, 04)
                    };
                    Book b5 = new Book
                    {
                        Title = "Emil i Jönköping",
                        Price = 299.00M,
                        GenreId = 4,
                        AuthorsId = 3,
                        ReleaseDate = new DateTime(2010, 04, 01)
                    };
                    Book b6 = new Book
                    {
                        Title = "SockerKråkan",
                        Price = 399.00M,
                        GenreId = 2,
                        AuthorsId = 3,
                        ReleaseDate = new DateTime(2011, 06, 07)
                    };
                    Book b7 = new Book
                    {
                        Title = "Barnen i tysta byn",
                        Price = 399.00M,
                        GenreId = 1,
                        AuthorsId = 4,
                        ReleaseDate = new DateTime(2012, 07, 07)
                    };
                    Book b8 = new Book
                    {
                        Title = "Silence of the cows",
                        Price = 199.00M,
                        GenreId = 2,
                        AuthorsId = 4,
                        ReleaseDate = new DateTime(2013, 06, 27)
                    };
                    Book b9 = new Book
                    {
                        Title = "Pippi Kortstrumpa",
                        Price = 399.00M,
                        GenreId = 5,
                        AuthorsId = 5,
                        ReleaseDate = new DateTime(2015, 10, 16)
                    };
                    Book b10 = new Book
                    {
                        Title = "Let them sleep",
                        Price = 599.00M,
                        GenreId = 1,
                        AuthorsId = 5,
                        ReleaseDate = new DateTime(2020, 12, 24)
                    };
                    Book b11 = new Book
                    {
                        Title = "McGyver can´t make something",
                        Price = 399.00M,
                        GenreId = 5,
                        AuthorsId = 6,
                        ReleaseDate = new DateTime(2013, 06, 24)
                    };
                    Book b12 = new Book
                    {
                        Title = "Mumin på bergen",
                        Price = 699.00M,
                        GenreId = 4,
                        AuthorsId = 6,
                        ReleaseDate = new DateTime(2010, 04, 03)
                    };
                    a1.Books = new List<Book> { b1, b2 };
                    a2.Books = new List<Book> { b3, b4 };
                    a3.Books = new List<Book> { b5, b6 };
                    a4.Books = new List<Book> { b7, b8 };
                    a5.Books = new List<Book> { b9, b10 };
                    a6.Books = new List<Book> { b11, b12 };
                    context.AddRange(a1, a2, a3, a4, a5, a6);
                    context.SaveChanges();
                };




            }
        }

        public static void AddStarterStores()
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

        public static void AddStarterToStores()
        {

            using (var context = new ShadyBookAppContext())
            {
                var stock = new List<Stock>()
                {
                    new Stock()
                    {
                        BookId = 9789188876653,
                        StoreId = 1,
                        StockItem = 10
                    },
                    new Stock()
                    {
                        BookId = 9789188876653,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876653,
                        StoreId = 3,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876654,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876654,
                        StoreId = 2,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876654,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876655,
                        StoreId = 1,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876655,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876655,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876656,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876656,
                        StoreId = 2,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876656,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876657,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876657,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876657,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876658,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876658,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876658,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876659,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876659,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876659,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876660,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876660,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876660,
                        StoreId = 3,
                        StockItem = 10
                    },
                       new Stock()
                    {
                        BookId = 9789188876661,
                        StoreId = 1,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876661,
                        StoreId = 2,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876661,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876662,
                        StoreId = 1,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876662,
                        StoreId = 2,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876662,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876663,
                        StoreId = 1,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876663,
                        StoreId = 2,
                        StockItem = 10
                    },
                       new Stock()
                    {
                        BookId = 9789188876663,
                        StoreId = 3,
                        StockItem = 10
                    },
                     new Stock()
                    {
                        BookId = 9789188876664,
                        StoreId = 1,
                        StockItem = 10
                    },
                      new Stock()
                    {
                        BookId = 9789188876664,
                        StoreId = 2,
                        StockItem = 10
                    },
                       new Stock()
                    {
                        BookId = 9789188876664,
                        StoreId = 3,
                        StockItem = 10
                    },




                };
                context.AddRange(stock);
                context.SaveChanges();

            }
        }


    }
}
