using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShadyBookAppV2.Models;

namespace ShadyBookAppV2
{
    public class ShadyBookAppContext : DbContext
    {

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
                .UseSqlServer(config["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasKey(t => new {t.StoreId,t.BookId});

            modelBuilder.Entity<Stock>()
                .HasOne(b => b.Book)
                .WithMany(st => st.Stocks)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Stock>()
                .HasOne(store => store.Store)
                .WithMany(st => st.Stocks)
                .HasForeignKey(store => store.StoreId);

        }
    }
}
