namespace PracticalCoding.Web.Migrations
{
    using CsvHelper;
    using PracticalCoding.Web.Models.CodeFirst;
    using PracticalCoding.Web.Models.Dashboard;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<PracticalCoding.Web.Models.EF6DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PracticalCoding.Web.Models.EF6DbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Authors.AddOrUpdate(x => x.Id,
                new Author() { Id = 1, Name = "Jane Austen" },
                new Author() { Id = 2, Name = "Charles Dickens" },
                new Author() { Id = 3, Name = "Miguel de Cervantes" }
                );

            context.Books.AddOrUpdate(x => x.Id,
                new Book() { Id = 1, Title = "Pride and Prejudice", Year = 1813, AuthorId = 1, Price = 9.99M, Genre = "Comedy of manners"},
                new Book() { Id = 2, Title = "Northanger Abbey", Year = 1817, AuthorId = 1, Price = 12.95M, Genre = "Gothic parody"},
                new Book() { Id = 3, Title = "David Copperfield", Year = 1850, AuthorId = 2, Price = 15, Genre = "Bildungsroman"},
                new Book() { Id = 4, Title = "Don Quixote", Year = 1617, AuthorId = 3, Price = 8.95M, Genre = "Picaresque"}
                );

            context.LazyAuthors.AddOrUpdate(x => x.Id,
                new LazyAuthor() { Id = 1, Name = "Jane Austen" },
                new LazyAuthor() { Id = 2, Name = "Charles Dickens" },
                new LazyAuthor() { Id = 3, Name = "Miguel de Cervantes" }
                );

            context.LazyBooks.AddOrUpdate(x => x.Id,
                new LazyBook() { Id = 1, Title = "Pride and Prejudice", Year = 1813, AuthorId = 1, Price = 9.99M, Genre = "Comedy of manners" },
                new LazyBook() { Id = 2, Title = "Northanger Abbey", Year = 1817, AuthorId = 1, Price = 12.95M, Genre = "Gothic parody" },
                new LazyBook() { Id = 3, Title = "David Copperfield", Year = 1850, AuthorId = 2, Price = 15, Genre = "Bildungsroman" },
                new LazyBook() { Id = 4, Title = "Don Quixote", Year = 1617, AuthorId = 3, Price = 8.95M, Genre = "Picaresque" }
                );

            context.CirRefAuthors.AddOrUpdate(x => x.Id,
                new CirRefAuthor() { Id = 1, Name = "Jane Austen" },
                new CirRefAuthor() { Id = 2, Name = "Charles Dickens" },
                new CirRefAuthor() { Id = 3, Name = "Miguel de Cervantes" }
                );

            context.CirRefBooks.AddOrUpdate(x => x.Id,
                new CirRefBook() { Id = 1, Title = "Pride and Prejudice", Year = 1813, AuthorId = 1, Price = 9.99M, Genre = "Comedy of manners" },
                new CirRefBook() { Id = 2, Title = "Northanger Abbey", Year = 1817, AuthorId = 1, Price = 12.95M, Genre = "Gothic parody" },
                new CirRefBook() { Id = 3, Title = "David Copperfield", Year = 1850, AuthorId = 2, Price = 15, Genre = "Bildungsroman" },
                new CirRefBook() { Id = 4, Title = "Don Quixote", Year = 1617, AuthorId = 3, Price = 8.95M, Genre = "Picaresque" }
                );
        }
    }
}
