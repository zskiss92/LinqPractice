using LinqPractice.Models;

namespace LinqPractice.Repository
{
    public interface IMuseumObjectRepository
    {
        ServiceResponse Add(MuseumObject museumObject);
        ServiceResponse Update(MuseumObject museumObject);
        ServiceResponse Delete(string objectInventoryNumber);
        void Save();
        Dictionary<string, MuseumObject> GetAll();
        MuseumObject Get(string objectInventoryNumber);
        Dictionary<string, MuseumObject> GetAllBetweenYears(int startYear, int endYear);
        Dictionary<string, MuseumObject> GetAllBetweenYearsWithFirstLetter(int startYear, int endYear, string startsWith);
        Dictionary<string, MuseumObject> GetAllBetweenYearsWithLastLetter(int startYear, int endYear, string endsWith);
        Dictionary<string, MuseumObject> GetAllBetweenYearsContains(int startYear, int endYear, string pattern);
        int CountObjects();
        int CountObjectsBetweenYears(int startYear, int endYear);
        Dictionary<string, int> GroupByTypeAndCount();

    }
}
