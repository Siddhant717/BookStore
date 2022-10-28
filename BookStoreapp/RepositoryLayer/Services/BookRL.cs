using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL:IBookRL
    {
        private IConfiguration config;
        public BookRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public bool AddBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_AddBook", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookName", bookModel.BookName);
                com.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                com.Parameters.AddWithValue("@Description", bookModel.Description);
                com.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                com.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                com.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                com.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                com.Parameters.AddWithValue("@Ratings", bookModel.Ratings);
                com.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);

                connection.Open();
                var reader = com.ExecuteNonQuery();
                connection.Close();
                return true;

            }
           
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        

        public bool UpdateBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_UpdateBookDetails", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", bookModel.BookId);
                com.Parameters.AddWithValue("@BookName", bookModel.BookName);
                com.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                com.Parameters.AddWithValue("@Description", bookModel.Description);
                com.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                com.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                com.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                com.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                com.Parameters.AddWithValue("@Ratings", bookModel.Ratings);
                com.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);

                connection.Open();
                var reader = com.ExecuteNonQuery();
                connection.Close();
                return true;

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteBook(int Id)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_DeleteBook", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", Id);
               

                connection.Open();
                var reader = com.ExecuteNonQuery();
                connection.Close();
                return true;

            }
           
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
           
        }

        public List<BookModel> getAllBooks()
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_GetAllBooks", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

               

                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<BookModel> allbooks = new List<BookModel>();
                while (reader.Read())
                {
                    var books = new BookModel();
                    books.BookId = reader.GetInt32(0);
                    books.BookName = reader.GetString(1);
                    books.AuthorName = reader.GetString(2);
                    books.Description = reader.GetString(3);
                    books.ActualPrice = reader.GetDecimal(4);
                    books.DiscountedPrice = reader.GetDecimal(5);
                    books.Quantity = reader.GetInt32(6);
                    books.BookImage = reader.GetString(7);
                    books.Ratings = reader.GetDouble(8);
                    books.RatingCount = reader.GetInt32(9);
                    allbooks.Add(books);
                }
               
                connection.Close();
                return allbooks;
                

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public BookModel getBookById(int Id)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_getBookById", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", Id);


                connection.Open();
               
                SqlDataReader reader = com.ExecuteReader();
                BookModel books = new BookModel();
                while (reader.Read())
                {
                  
                    books.BookId = reader.GetInt32(0);
                    books.BookName = reader.GetString(1);
                    books.AuthorName = reader.GetString(2);
                    books.Description = reader.GetString(3);
                    books.ActualPrice = reader.GetDecimal(4);
                    books.DiscountedPrice = reader.GetDecimal(5);
                    books.Quantity = reader.GetInt32(6);
                    books.BookImage = reader.GetString(7);
                    books.Ratings = reader.GetDouble(8);
                    books.RatingCount = reader.GetInt32(9);
                    
                }
                return books;
                connection.Close();
              

            }


            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
