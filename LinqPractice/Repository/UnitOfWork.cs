using LinqPractice.Data;

namespace LinqPractice.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            MuseumObject = new MuseumObjectRepository(_db);
            AuthorRepository = new AuthorRepository(_db);
            BookRepository = new BookRepository(_db);
        }

        public IMuseumObjectRepository MuseumObject { get; private set; }

        public IAuthorRepository AuthorRepository { get; private set; }

        public IBookRepository BookRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
