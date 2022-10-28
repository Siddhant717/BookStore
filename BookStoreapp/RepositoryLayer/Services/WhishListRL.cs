using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WhishListRL:IWhishListRL
    {
        private IConfiguration config;
        public WhishListRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public bool AddtoWhishlist(WhishListModel whishlistModel,int userid)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_AddtoWhishlist", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Id", userid);
                com.Parameters.AddWithValue("@BookId", whishlistModel.BookId);
                

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

        public bool DeleteFromWhishlist(int whishlistId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_DeleteWhishlist", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

           
                com.Parameters.AddWithValue("@WhishlistId", whishlistId);


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

        public List<getAllWhishlist> getAllWhishlist()
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_getAllWhishlist", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;



                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<getAllWhishlist> alllist = new List<getAllWhishlist>();
                while (reader.Read())
                {
                    var list = new getAllWhishlist();
                    list.WhishlistId = reader.GetInt32(0);
                    list.Id = reader.GetInt32(1);
                    list.BookId = reader.GetInt32(2);

                    alllist.Add(list);
                }

                connection.Close();
                return alllist;


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
