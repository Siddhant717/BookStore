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

        

        public bool UpdateBook( int Id,BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_UpdateBookDetails", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", Id);
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
    }
}
