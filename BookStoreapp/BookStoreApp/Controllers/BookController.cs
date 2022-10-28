using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
              var result=  this.bookBL.AddBook(bookModel);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Book Added Successfully"});
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Soory!Book is not added" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBookDetails")]
        public IActionResult UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                var result = this.bookBL.UpdateBook(bookModel);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Book details updated Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Book details is not updated" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBook/{Id}")]
        public IActionResult DeleteBook(int Id)
        {
            try
            {
                var result = this.bookBL.DeleteBook(Id);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Book deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not deleted" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.getAllBooks();
                if (result!=null)
                {
                    return this.Ok(new { sucess = true, status = 200, data=result });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not fetched" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllBooksById/{Id}")]
        public IActionResult GetAllBooksById(int Id)
        {
            try
            {
                var result = this.bookBL.getBookById(Id);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, status = 200, data = result });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not fetched" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
