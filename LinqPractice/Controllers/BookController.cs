using LinqPractice.Models;
using LinqPractice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LinqPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddBook")]
        public ActionResult AddBook(Book book)
        {
            var result = _unitOfWork.BookRepository.Add(book);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            else
            {
                return Ok(result);
            }
        }

        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook(Book book)
        {
            var result = _unitOfWork.BookRepository.Update(book);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetEveryBook")]
        public ActionResult GetEveryBook()
        {
            return Ok(_unitOfWork.BookRepository.GetAll());
        }

        [HttpGet("SumOfPagesByAuthor")]
        public ActionResult SumOfPagesByAuthor()
        {
            return Ok(_unitOfWork.BookRepository.SumOfPagesByAuthor());
        }

        [HttpGet("AveragePagesByAuthor")]
        public ActionResult AveragePagesByAuthor()
        {
            return Ok(_unitOfWork.BookRepository.AverageOfPagesByAuthor());
        }

        [HttpGet("OrderByAsc")]
        public ActionResult OrderByAsc()
        {
            return Ok(_unitOfWork.BookRepository.OrderByAuthorThenByBook());
        }

        [HttpGet("OrderByDesc")]
        public ActionResult OrderByDesc()
        {
            return Ok(_unitOfWork.BookRepository.OrderByAuthorThenByBookDescending());
        }
    }
}
