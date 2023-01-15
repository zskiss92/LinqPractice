using LinqPractice.Models;

namespace LinqPractice.Repository
{
    public interface IAuthorRepository
    {
        ServiceResponse Add(Author author);
        ServiceResponse Delete(Guid id);
        ServiceResponse Update(Author author);
        List<Author> GetAll();
        Author GetById(Guid id);
        List<Author> GetByName(string name);
    }
}
