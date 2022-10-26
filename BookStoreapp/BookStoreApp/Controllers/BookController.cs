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

        [Authorize]
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
        [Authorize]
        [HttpPut("UpdateBookDetails/{Id}")]
        public IActionResult UpdateBookDetails(int Id,BookModel bookModel)
        {
            try
            {
                var result = this.bookBL.UpdateBook( Id,bookModel);
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
        [Authorize]
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
    }
}
