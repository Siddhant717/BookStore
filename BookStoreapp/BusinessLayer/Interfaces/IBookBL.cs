using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        bool AddBook(BookModel bookModel);
        bool UpdateBook( BookModel bookModel);
        bool DeleteBook(int Id);
        List<BookModel> getAllBooks();
        BookModel getBookById(int Id);
    }
}
