using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedBackRL:IFeedBackRL
    {
        private IConfiguration config;
        public FeedBackRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public bool AddFeedback(AddFeedbackModel addFeedbackModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_AddFeedback", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@BookId", addFeedbackModel.BookId);
                com.Parameters.AddWithValue("@Id", addFeedbackModel.Id);
                com.Parameters.AddWithValue("@Rating", addFeedbackModel.Rating);
                com.Parameters.AddWithValue("@Comment", addFeedbackModel.Comment);
               
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

        public List<AllfeedbackModel> getFeedback(int bookId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_getallFeedback", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", bookId);


                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<AllfeedbackModel> feedback = new List<AllfeedbackModel>();
                while (reader.Read())
                {
                    var fb = new AllfeedbackModel();
                    fb.FeedbackId = reader.GetInt32(0);
                    fb.BookId = reader.GetInt32(1);
                    fb.Id = reader.GetInt32(2);
                    fb.Rating = reader.GetInt32(3);
                    fb.Comment = reader.GetString(4);

                    feedback.Add(fb);
                }

                connection.Close();
                return feedback;


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
