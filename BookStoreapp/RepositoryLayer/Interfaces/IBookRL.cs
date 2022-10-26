using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        bool AddBook(BookModel bookModel);
        bool UpdateBook(int Id, BookModel bookModel);
        bool DeleteBook(int Id);
    }
}
