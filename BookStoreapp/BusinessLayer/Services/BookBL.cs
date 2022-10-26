using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL :IBookBL
    {
        readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public bool AddBook(BookModel bookModel)
        {
            try
            {
                return this.bookRL.AddBook(bookModel);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

       

        public bool UpdateBook( int id,BookModel bookModel)
        {
            try
            {
                return this.bookRL.UpdateBook(id,bookModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteBook(int Id)
        {
            try
            {
                return this.bookRL.DeleteBook(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BookModel> getAllBooks()
        {
            try
            {
                return this.bookRL.getAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
