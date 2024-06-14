using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P7AppAPI.Models;
using P7AppAPI.Services;

namespace P7AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                var response = await _bookService.GetAllBook();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetBookById/{Id}")]
        public async Task<IActionResult> GetBookById(int Id)
        {
            try
            {
                var response = await _bookService.GetBookById(Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookDTO bookInfo)
        {
            try
            {
                var response = await _bookService.AddBook(bookInfo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDTO bookInfo)
        {
            try
            {
                if (bookInfo.Id > 0)
                {
                    var response = await _bookService.UpdateBook(bookInfo);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Please add book id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook([FromBody] DeleteBookDTO bookInfo)
        {
            try
            {
                if (bookInfo.Id > 0)
                {
                    var response = await _bookService.DeleteBook(bookInfo);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Please add book id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
