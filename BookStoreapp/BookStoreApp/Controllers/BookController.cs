using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        IBookBL bookBL;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;
        public BookController(IBookBL bookBL, IDistributedCache cache, IMemoryCache memoryCache)
        {
            this.bookBL = bookBL;
            this._cache = cache;
            this._memoryCache = memoryCache;
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
        [Authorize]
        [HttpGet("GetAllBooksUsingRedisCache")]
        public IActionResult GetAllBooksUsingRedisCache()
        
        
        
        {
            try
            {
                string CacheKey = "BookList";
                string serializeNoteList;
                var NoteList = new List<BookModel>();
                var RedisNoteList = _cache.Get(CacheKey);

                if (RedisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(RedisNoteList);
                    NoteList = JsonConvert.DeserializeObject<List<BookModel>>(serializeNoteList);
                }
                else
                {
                   
                    NoteList = this.bookBL.getAllBooks();
                    serializeNoteList = JsonConvert.SerializeObject(NoteList);
                    RedisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    _cache.Set(CacheKey, RedisNoteList, option);

                }
                return this.Ok(new { success = true, status = 200, noteList = NoteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
