namespace LinqPractice.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IMuseumObjectRepository MuseumObject { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
    }
}
