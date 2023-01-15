using LinqPractice.Data;
using LinqPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqPractice.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private AppDbContext _db;
        internal DbSet<Author> dbSet;

        public AuthorRepository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<Author>();
        }

        public ServiceResponse Add(Author author)
        {
            var authorFromDb = _db.Authors.FirstOrDefault(x => x.Name.Contains(author.Name));

            if(authorFromDb != null)
            {
                return new ServiceResponse(false, "Author or similar is already exists");
            }

            _db.Authors.Add(author);
            _db.SaveChanges();

            return new ServiceResponse(true, "Author successfully added");
        }

        public ServiceResponse Delete(Guid id)
        {
            var authorFromDb = _db.Authors.FirstOrDefault(x => x.Id.Equals(id));

            if(authorFromDb == null)
            {
                return new ServiceResponse(false, "Author not found");
            }

            _db.Authors.Remove(authorFromDb);

            return new ServiceResponse(true, "Author successfully removed");
        }

        public ServiceResponse Update(Author author)
        {
            var authorFromDb = _db.Authors.FirstOrDefault(x => x.Id.Equals(author.Id));

            if (authorFromDb == null)
            {
                return new ServiceResponse(false, "Author not found");
            }

            authorFromDb.Name = author.Name;

            return new ServiceResponse(true, "Author successfully updated");
        }

        public List<Author> GetAll()
        {
            IQueryable<Author> query = dbSet;

            var result =
                from author in query
                select author;

            return result.ToList();
        }

        public Author? GetById(Guid id)
        {
            IQueryable<Author> query = dbSet;

            var result =
                (from author in query
                 where author.Id == id
                 select author).FirstOrDefault();

            return result;
        }

        public List<Author> GetByName(string name)
        {
            IQueryable<Author> query = dbSet;

            var result =
                from author in query
                where author.Name.Contains(name)
                select author;

            return result.ToList();
        }
    }
}
