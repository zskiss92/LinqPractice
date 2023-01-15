using LinqPractice.Data;
using LinqPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqPractice.Repository
{
    public class MuseumObjectRepository : IMuseumObjectRepository
    {
        private AppDbContext _db;
        internal DbSet<MuseumObject> dbSet;

        public MuseumObjectRepository(AppDbContext db)
        {
            _db = db;
            dbSet = db.Set<MuseumObject>();
        }

        public ServiceResponse Add(MuseumObject museumObject)
        {
            _db.MuseumObjects.Add(museumObject);

            return new ServiceResponse(true, "Object saved");
        }

        public ServiceResponse Update(MuseumObject museumObject)
        {
            var objectFromDb = _db.MuseumObjects.FirstOrDefault(x => x.ObjectInventoryNumber.Equals(museumObject.ObjectInventoryNumber));

            if(objectFromDb == null)
            {
                return new ServiceResponse(false, "Object not found");
            }

            objectFromDb.ObjectPersistentIdentifier = museumObject.ObjectPersistentIdentifier;
            objectFromDb.ObjectTitle = museumObject.ObjectTitle;
            objectFromDb.ObjectType = museumObject.ObjectType;
            objectFromDb.ObjectCreator = museumObject.ObjectCreator;
            objectFromDb.ObjectCreationDate = museumObject.ObjectCreationDate;
            objectFromDb.ObjectImage = museumObject.ObjectImage;

            return new ServiceResponse(true, "Object successfully updated");

        }

        public ServiceResponse Delete(string objectInventoryNumber)
        {
            var objFromDb = _db.MuseumObjects.FirstOrDefault(x => x.ObjectInventoryNumber.Equals(objectInventoryNumber));

            if(objFromDb == null)
            {
                return new ServiceResponse(false, "Object not found");
            }

            _db.MuseumObjects.Remove(objFromDb);

            return new ServiceResponse(true, "Object successfully removed");
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public Dictionary<string, MuseumObject> GetAll()
        {
            IQueryable<MuseumObject> query = dbSet;

            return query.ToDictionary(x => x.ObjectInventoryNumber);
        }

        public MuseumObject? Get(string objectInventoryNumber)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                (from m in query
                 where m.ObjectInventoryNumber == objectInventoryNumber
                 select m).FirstOrDefault();

            return result;
        }

        public Dictionary<string, MuseumObject> GetAllBetweenYears(int startYear, int endYear)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                from m in query
                where m.ObjectCreationDate >= startYear && m.ObjectCreationDate <= endYear
                select m;

            return result.ToDictionary(x => x.ObjectInventoryNumber);
        }

        public Dictionary<string, MuseumObject> GetAllBetweenYearsWithFirstLetter(int startYear, int endYear, string startsWith)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                from m in query
                where m.ObjectCreationDate >= startYear && m.ObjectCreationDate <= endYear
                && m.ObjectTitle.StartsWith(startsWith)
                select m;

            return result.ToDictionary(x => x.ObjectInventoryNumber);
        }

        public Dictionary<string, MuseumObject> GetAllBetweenYearsWithLastLetter(int startYear, int endYear, string endsWith)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                from m in query
                where m.ObjectCreationDate >= startYear
                && m.ObjectCreationDate <= endYear
                && m.ObjectTitle.EndsWith(endsWith)
                select m;

            return result.ToDictionary(x => x.ObjectInventoryNumber);
        }

        public Dictionary<string, MuseumObject> GetAllBetweenYearsContains(int startYear, int endYear, string pattern)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                from m in query
                where m.ObjectCreationDate >= startYear && m.ObjectCreationDate <= endYear
                && m.ObjectTitle.Contains(pattern)
                select m;

            return result.ToDictionary(x => x.ObjectInventoryNumber);
        }

        public int CountObjects()
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                (from m in query
                select m).Count();

            return result;
        }

        public int CountObjectsBetweenYears(int startYear, int endYear)
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                (from m in query
                 where m.ObjectCreationDate >= startYear && m.ObjectCreationDate <= endYear
                 select m).Count();

            return result;
        }

        public Dictionary<string, int> GroupByTypeAndCount()
        {
            IQueryable<MuseumObject> query = dbSet;

            var result =
                from m in query
                group m by m.ObjectType into g
                select new
                {
                    g.Key,
                    Value = g.Count(),
                };

            return result.ToDictionary(x => x.Key, v => v.Value);
        }
    }
}
