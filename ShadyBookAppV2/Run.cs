﻿using Microsoft.EntityFrameworkCore;
using ShadyBookAppV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadyBookAppV2
{
    public class Run
    {
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



            using (var context = new ShadyBookAppContext())
            {


                ListAllBooks();
                Console.Write("Type in a Book ID: ");
                ulong id = CheckUserInputUlong(Console.ReadLine());
                bool ifFindBook = context.Books.Any(x => x.Id == id);
                if (ifFindBook != true)
                {
                    Console.WriteLine("Could not find book!");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine();

                ListAllStores();
                Console.Write("What store do you wish to stock up?: ");
                int storeId = CheckUserInputInt(Console.ReadLine());
                bool ifFindStore = context.Stores.Any(x => x.Id == storeId);
                if (ifFindBook != true)
                {
                    Console.WriteLine("Could not find store");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine();

                Console.WriteLine("How many book would you like to add: ");
                int quantity = CheckUserInputInt(Console.ReadLine());




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
        //färdig

        void UpdateAuthor()
        {
            ListAllAuthors();
            Console.Write("Please enter the author you want to update: ");
            string isString = Console.ReadLine();
            int isInt = CheckUserInputInt(isString);

            using (var context = new ShadyBookAppContext())
            {
                var author = context.Authors.Find(isInt);
                if (author != null)
                {
                    //input 
                    string firstName = ReturnInput("Enter First Name: ");
                    string lastName = ReturnInput("Enter Last Name: ");
                    Console.Write("Please enter a date to update, please use the format 'YYYY-MM-DD'.\nIf you don't enter it a default value will be entered: ");
                    string newDate = Console.ReadLine();
                    DateTime updatedDate = ConvertToDateTime(newDate);
                    //ändrar
                    author.BirthDate = updatedDate;
                    author.FirstName = firstName;
                    author.LastName = lastName;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("You entered an invalid author");
                    Console.ReadLine();

                }
            }
        }
        //färdig
        void UpdateBook()
        {
            //bookId
            ListAllBooks();
            Console.Write("Please enter the book you want to update: ");
            string newBookId = Console.ReadLine();
            ulong updatedBookId = CheckUserInputUlong(newBookId);


            using (var context = new ShadyBookAppContext())
            {

                var author = context.Books.Find(updatedBookId);
                if (author != null)
                {
                    //date
                    string newDate = ReturnInput("Please enter a date to update, please use the format 'YYYY-MM-DD'.\nIf you don't enter it a default value will be entered: ");
                    DateTime updatedDate = ConvertToDateTime(newDate);

                    //title
                    string newTitle = ReturnInput("Please enter a new title: ");

                    //authorId
                    ListAllAuthors();
                    string newAuthor = ReturnInput("Please enter a new authorId: ");
                    int newAuthorId = CheckUserInputInt(newAuthor);

                    //genreId
                    ListAllGenres();
                    string newGenre = ReturnInput("Please enter a new genreId:");
                    int newGenreId = CheckUserInputInt(newGenre);

                    //price
                    string newPrice = ReturnInput("Please enter a new price, please use commas for decimal numbers: ");
                    decimal newUpdatedPrice = CheckUserInputDecimal(newPrice);

                    //updaterar
                    author.Title = newTitle;
                    author.Price = newUpdatedPrice;
                    author.ReleaseDate = updatedDate;
                    author.GenreId = newGenreId;
                    author.AuthorsId = newAuthorId;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("You made a misstake");
                    Console.ReadLine();

                }
            }
        }
        void UpdateAuthorsWithNewBooks()
        {
            using (var context = new ShadyBookAppContext())
            {

                ListAllAuthors();
                string stringChoice = ReturnInput("Please enter an author to add a book to: ");
                int choice = CheckUserInputInt(stringChoice);

                var author = context.Authors.Find(choice);
                if (author != null)
                {

                    string title = ReturnInput("Please enter title: ");

                    string decimalAsString = ReturnInput("Please enter a price: ");
                    decimal price = CheckUserInputDecimal(decimalAsString);

                    ListAllGenres();
                    string genreAsString = ReturnInput("Please enter a genre: ");
                    int genreId = CheckUserInputInt(genreAsString);


                    author.Books.Add(new Book() { Title = title, Price = price, GenreId = genreId, AuthorsId = choice });
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Det finns ingen författare med det id:t");
                    Console.ReadLine();

                }
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
                    Console.ReadLine();

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

            using (var context = new ShadyBookAppContext())
            {
                var book = context.Books.Find(isUlong);


                if (book != null)
                {
                    context.Entry(book).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("You entered a invalid isbn number");
                    Console.ReadLine();
                }

            }


        }
        void DeleteFromStore()
        {


            using (var context = new ShadyBookAppContext())
            {

                ListAllBooks();
                Console.Write("Type in a Book ID: ");
                ulong id = CheckUserInputUlong(Console.ReadLine());
                bool ifFindBook = context.Books.Any(x => x.Id == id);
                if (ifFindBook != true)
                {
                    Console.WriteLine("Could not find book!");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine();

                ListAllStores();
                Console.Write("What store do you wish to stock up?: ");
                int storeId = CheckUserInputInt(Console.ReadLine());
                bool ifFindStore = context.Stores.Any(x => x.Id == storeId);
                if (ifFindBook != true)
                {
                    Console.WriteLine("Could not find store");
                    Console.ReadLine();
                    return;
                }



                var track = context.Stocks.Where(x => x.StoreId == storeId && x.BookId == id).FirstOrDefault();
                if (track != null)
                {

                    context.Remove(track);
                    context.SaveChanges();
                    Console.WriteLine("Alla böckerna borttagna!");
                    Console.ReadLine();
                }
                
            }


        }
        #endregion

        #region QueriesForJoinTables
        void ShowAuthorWithBooks()
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
        void ShowStoresWithBooksWithStocks()
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
        void StartUp()
        {
            DataBaseFiles.AddStartupGenres();
            DataBaseFiles.AddStarterAuthorsAndBooks();
            DataBaseFiles.AddStarterStores();
            DataBaseFiles.AddStarterToStores();



        }



        ulong CheckUserInputUlong(string input)
        {
            bool safe = ulong.TryParse(input, out ulong value);
            while (safe == false)

            {
                Console.WriteLine("Please enter a correct NUMBER: ");
                input = Console.ReadLine();
                safe = ulong.TryParse(input, out value);
            }
            return value;

        }
        int CheckUserInputInt(string input)
        {
            bool safe = int.TryParse(input, out int value);
            while (safe == false)

            {
                Console.WriteLine("Please enter a correct NUMBER: ");
                input = Console.ReadLine();
                safe = int.TryParse(input, out value);
            }
            return value;

        }

        decimal CheckUserInputDecimal(string input)
        {
            bool safe = decimal.TryParse(input, out decimal value);
            while (safe == false)

            {
                Console.WriteLine("Please enter a correct NUMBER: ");
                input = Console.ReadLine();
                safe = decimal.TryParse(input, out value);
            }
            return value;

        }

        string ReturnInput(string message)
        {
            Console.Write($"{message}: ");
            string output = Console.ReadLine();
            return output;
        }

        public void Menu()
        {
            string[] funcitons = new string[] {"Startup (Only run once!)","ListAllGenres", "ListAllAuthors", "ListAllBooks", "ListAllStores",
    "ShowAuthorWithBooks","ShowStoresWithBooksAndStocks",
"-------------------------", "AddToStore", "AddAuthor","AddBook", "-------------------------",
"Update Author", "Update Book","-------------------------", "Delete author with books",
    "Delete book", "Delete books from store" };

            int x = 0;
            while (x != funcitons.Length + 1)
            {
                Console.Clear();
                for (int i = 0; i < funcitons.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] {funcitons[i]}");
                }
                Console.WriteLine($"[{funcitons.Length + 1}] Exit");
                x = CheckUserInputInt(Console.ReadLine());
                Console.Clear();
                switch (x)
                {
                    case 1:
                        {
                            try
                            {
                                using (var context = new ShadyBookAppContext())
                                {
                                    bool test = context.Books.Any();

                                    if (test != true)
                                    {
                                        StartUp();
                                    }
                                    else
                                        Console.WriteLine("Already Used");
                                }

                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Already Used");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            ListAllGenres();
                            Console.ReadLine();
                            break;
                        }
                    case 3:
                        {
                            ListAllAuthors();
                            Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            ListAllBooks();
                            Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            ListAllStores();
                            Console.ReadLine();
                            break;
                        }
                    case 6:
                        {
                            ShowAuthorWithBooks();
                            Console.ReadLine();
                            break;
                        }
                    case 7:
                        {
                            ShowStoresWithBooksWithStocks();
                            Console.ReadLine();
                            break;
                        }
                    case 9:
                        {
                            AddToStore();

                            break;
                        }
                    case 10:
                        {
                            AddAuthorsUi();

                            break;
                        }
                    case 11:
                        {
                            UpdateAuthorsWithNewBooks();

                            break;
                        }
                    case 13:
                        {
                            UpdateAuthor();

                            break;
                        }
                    case 14:
                        {
                            UpdateBook();

                            break;
                        }
                    case 16:
                        {
                            DeleteAuthorWithBooks();

                            break;
                        }
                    case 17:
                        {
                            DeleteBook();

                            break;

                        }
                    case 18:
                        {
                            DeleteFromStore();

                            break;
                        }
                    default:
                        {

                            break;
                        }
                }
            }

        }
    }
}