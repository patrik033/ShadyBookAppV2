using Microsoft.EntityFrameworkCore;
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
                



                var track = context.Stocks.Where(x => x.StoreId == storeId && x.BookId == id).FirstOrDefault();
                if (track == null)
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
                    track.StockItem += quantity;
                    context.SaveChanges();
                }



            }
        }
        void AddBook()
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
                    var genre = context.Genres.Find(genreId);
                    if (genre != null)
                    {
                        author.Books.Add(new Book() { Title = title, Price = price, GenreId = genreId, AuthorsId = choice });
                        context.SaveChanges();

                    }
                    else
                    {
                        Console.WriteLine("You entered an author that doesn't exist");
                        Console.ReadLine();
                    }

                }
                else
                {
                    Console.WriteLine("Det finns ingen författare med det id:t");
                    Console.ReadLine();

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
                    var testAuthor = context.Authors.Find(newAuthorId);
                    if (testAuthor == null)
                    {
                        Console.WriteLine("You entered an invalid author");
                        Console.ReadLine();
                        return;
                    }

                    //genreId
                    ListAllGenres();
                    string newGenre = ReturnInput("Please enter a new genreId:");
                    int newGenreId = CheckUserInputInt(newGenre);
                    var testGenre = context.Genres.Find(newGenreId);

                    if(testGenre == null)
                    {
                        Console.WriteLine("You entered an invalid genre");
                        Console.ReadLine();
                        return;
                    }

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
            }
        }
        #endregion

        #region Deletes
        //färdig
        void DeleteAuthorWithBooks()
        {
            ListAllAuthors();


         
            string choiceAsString = ReturnInput("Choice which author you want to delete: ");
            bool choice = int.TryParse(choiceAsString, out int choiceAsInt);

            while (choice != true)
            {
                choiceAsString = ReturnInput("Please enter a number: ");
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
            Console.WriteLine("Done!");
        }



        ulong CheckUserInputUlong(string input)
        {
            bool safe = ulong.TryParse(input, out ulong value);
            while (value < 0 || safe == false)

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
            while (value < 0 || safe == false)
            {
                Console.Write("Not a accepted value, try again: ");        
                input = Console.ReadLine();
                Console.WriteLine();
                safe = int.TryParse(input, out value);
            }
            return value;
        }

        decimal CheckUserInputDecimal(string input)
        {
            bool safe = decimal.TryParse(input, out decimal value);
            while (value < 0 || safe == false)

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
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 3:
                        {
                            ListAllAuthors();
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            ListAllBooks();
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            ListAllStores();
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 6:
                        {
                            ShowAuthorWithBooks();
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 7:
                        {
                            ShowStoresWithBooksWithStocks();
                            Console.WriteLine("Done");
                            Console.ReadLine();
                            break;
                        }
                    case 9:
                        {
                            AddToStore();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 10:
                        {
                            AddAuthorsUi();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 11:
                        {
                            AddBook();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 13:
                        {
                            UpdateAuthor();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 14:
                        {
                            UpdateBook();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 16:
                        {
                            DeleteAuthorWithBooks();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 17:
                        {
                            DeleteBook();
                            Console.WriteLine("Done");
                            break;

                        }
                    case 18:
                        {
                            DeleteFromStore();
                            Console.WriteLine("Done");
                            break;
                        }
                    case 19:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("You entered an invalid option");
                            break;
                        }
                }
            }

        }
    }
}
