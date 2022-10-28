using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL:ICartRL
    {
        private IConfiguration config;
        public CartRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public bool AddToCart(int Id, CartModel cartModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_AddtoCart", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Quantity", cartModel.Quantity);
                com.Parameters.AddWithValue("@Id", Id);
                com.Parameters.AddWithValue("@BookId", cartModel.BookId);


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

        
        public bool UpdateCart(UpdateCartModel updatecartModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_UpdateCart", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Quantity", updatecartModel.Quantity);
                com.Parameters.AddWithValue("@CartId", updatecartModel.CartId);
              


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
        public bool DeleteCart(int cartid)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_DeleteCart", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@CartId", cartid);
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

        public List<GetAllCartModel> getAllCarts()
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_getAllCart", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;



                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<GetAllCartModel> carts = new List<GetAllCartModel>();
                while (reader.Read())
                {
                    var allcarts = new GetAllCartModel();
                    allcarts.CartId = reader.GetInt32(0);
                    allcarts.Quantity = reader.GetInt32(1);
                    allcarts.Id = reader.GetInt32(2);
                    allcarts.BookId = reader.GetInt32(3);
                   
                   
                    carts.Add(allcarts);
                }

                connection.Close();
                return carts;


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
