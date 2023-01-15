using LinqPractice.Models;

namespace LinqPractice.Repository
{
    public interface IBookRepository
    {
        ServiceResponse Add(Book book);
        ServiceResponse Delete(int id);
        ServiceResponse Update(Book book);
        Dictionary<int, Book> GetAll();
        Dictionary<string, int> SumOfPagesByAuthor();
        Dictionary<string, double> AverageOfPagesByAuthor();
        List<Book> OrderByAuthorThenByBook();
        List<Book> OrderByAuthorThenByBookDescending();
    }
}
