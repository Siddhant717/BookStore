using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        bool AddBook(BookModel bookModel);
        bool UpdateBook(BookModel bookModel);
        bool DeleteBook(int Id);
        List<BookModel> getAllBooks();
        BookModel getBookById(int Id);
    }
}
