using LinqPractice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LinqPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuseumController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MuseumController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_unitOfWork.MuseumObject.GetAll());
        }

        [HttpGet("GetBetween")]
        public ActionResult GetBetween(int startYear, int endYear)
        {
            if(endYear < startYear)
            {
                return BadRequest("Years are not correct");
            }

            return Ok(_unitOfWork.MuseumObject.GetAllBetweenYears(startYear, endYear));
        }

        [HttpGet("GetById")]
        public ActionResult GetById(string id)
        {
            var result = _unitOfWork.MuseumObject.Get(id);

            if(result == null)
            {
                return BadRequest("Object not found");
            }

            return Ok(result);
        }

        [HttpGet("GetBetweenStartsWith")]
        public ActionResult GetBetweenStartsWith(int startYear, int endYear, string startsWith)
        {
            if(startYear > endYear)
            {
                return BadRequest("Years are not correct");
            }

            if(startsWith.Length > 1 || startsWith.Length == 0)
            {
                return BadRequest("The length of pattern must be one letter");
            }

            return Ok(_unitOfWork.MuseumObject.GetAllBetweenYearsWithFirstLetter(startYear, endYear, startsWith));
        }

        [HttpGet("GetBetweenEndsWith")]
        public ActionResult GetBetweenEndsWith(int startYear, int endYear, string endsWith)
        {
            if(startYear > endYear)
            {
                return BadRequest("Years are not correct");
            }

            if(endsWith.Length > 1 || endsWith.Length == 0)
            {
                return BadRequest("The length of pattern must be one letter");
            }

            return Ok(_unitOfWork.MuseumObject.GetAllBetweenYearsWithLastLetter(startYear, endYear, endsWith));
        }

        [HttpGet("GetBetweenContains")]
        public ActionResult GetBetweenContains(int startYear, int endYear, string pattern)
        {
            if(startYear > endYear)
            {
                return BadRequest("The years are not correct");
            }

            return Ok(_unitOfWork.MuseumObject.GetAllBetweenYearsContains(startYear, endYear, pattern));
        }

        [HttpGet("CountEveryObject")]
        public ActionResult CountObjects()
        {
            return Ok(_unitOfWork.MuseumObject.CountObjects());
        }

        [HttpGet("CountEveryObjectBetween")]
        public ActionResult CountObjectsBetween(int startYear, int endYear)
        {
            if(startYear > endYear)
            {
                return BadRequest("The years are not correct");
            }

            return Ok(_unitOfWork.MuseumObject.CountObjectsBetweenYears(startYear, endYear));
        }

        [HttpGet("GroupByTypeAndCount")]
        public ActionResult GroupByTypeAndCount()
        {
            return Ok(_unitOfWork.MuseumObject.GroupByTypeAndCount());
        }
    }
}
