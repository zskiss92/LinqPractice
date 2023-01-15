using LinqPractice.Data;
using LinqPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqPractice.Repository
{
    public class BookRepository : IBookRepository
    {
        private AppDbContext _db;
        internal DbSet<Book> dbSet;

        public BookRepository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<Book>();
        }

        public ServiceResponse Add(Book book)
        {
            var bookFromDb = _db.Books.FirstOrDefault(x => x.Title.Contains(book.Title));

            if(bookFromDb != null)
            {
                return new ServiceResponse(false, "Book or similar is already exists");
            }

            _db.Books.Add(book);
            _db.SaveChanges();

            return new ServiceResponse(true, "Book successfully added");
        }

        public ServiceResponse Delete(int id)
        {
            var bookFromDb = _db.Books.FirstOrDefault(x => x.Id == id);

            if(bookFromDb == null)
            {
                return new ServiceResponse(false, "Book not found");
            }

            _db.Books.Remove(bookFromDb);

            return new ServiceResponse(true, "Book successfully removed");
        }

        public ServiceResponse Update(Book book)
        {
            var bookFromDb = _db.Books.FirstOrDefault(x => x.Id.Equals(book.Id));
            
            if(bookFromDb == null)
            {
                return new ServiceResponse(false, "Book not found");
            }

            bookFromDb.Title = book.Title;
            bookFromDb.Author = book.Author;
            bookFromDb.Pages = book.Pages;

            _db.SaveChanges();

            return new ServiceResponse(true, "Book successfully updated");
        }

        public Dictionary<int, Book> GetAll()
        {
            IQueryable<Book> query = dbSet;

            return query.ToDictionary(x => x.Id);
        }

        public Dictionary<string, int> SumOfPagesByAuthor()
        {
            var result =
                from book in _db.Books
                join author in _db.Authors on book.AuthorId equals author.Id
                group book by author.Name into groupOfBooks
                select new
                {
                    groupOfBooks.Key,
                    Value = groupOfBooks.Sum(x => x.Pages)
                };

            return result.ToDictionary(result => result.Key, result => result.Value);
        }

        public Dictionary<string, double> AverageOfPagesByAuthor()
        {
            var result =
                from book in _db.Books
                join author in _db.Authors on book.AuthorId equals author.Id
                group book by author.Name into groupOfBooks
                select new
                {
                    groupOfBooks.Key,
                    Value = groupOfBooks.Average(x => x.Pages)
                };

            return result.ToDictionary(result => result.Key, result => result.Value);
        }

        public List<Book> OrderByAuthorThenByBook()
        {
            var result =
                from book in _db.Books
                join author in _db.Authors on book.AuthorId equals author.Id
                orderby author.Name ascending, book.Title ascending
                select book;

            return result.ToList();
        }

        public List<Book> OrderByAuthorThenByBookDescending()
        {
            var result =
                from book in _db.Books
                join author in _db.Authors on book.AuthorId equals author.Id
                orderby author.Name descending, book.Title descending
                select book;

            return result.ToList();
        }
    }
}
