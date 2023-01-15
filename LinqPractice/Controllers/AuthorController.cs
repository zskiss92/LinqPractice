using LinqPractice.Models;
using LinqPractice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LinqPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddAuthor")]
        public ActionResult AddAuthor(Author author)
        {
            var result = _unitOfWork.AuthorRepository.Add(author);

            if(!result.IsSuccess)
            {
                return BadRequest(result);
            }

            else
            {
                return Ok(result);
            }
        }

        [HttpPut("UpdateAuthor")]
        public ActionResult UpdateAuthor(Author author)
        {
            var result = _unitOfWork.AuthorRepository.Update(author);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetEveryAuthor")]
        public ActionResult GetEveryAuthor()
        {
            return Ok(_unitOfWork.AuthorRepository.GetAll());
        }

        [HttpGet("GetAuthorById")]
        public ActionResult GetAuthorById(Guid id)
        {
            var result = _unitOfWork.AuthorRepository.GetById(id);

            if (result == null)
            {
                return BadRequest("Author not found");
            }

            else
            {
                return Ok(result);
            }
        }

        [HttpGet("GetAuthorByName")]
        public ActionResult GetAuthorByName(string name)
        {
            return Ok(_unitOfWork.AuthorRepository.GetByName(name));
        }
    }
}
