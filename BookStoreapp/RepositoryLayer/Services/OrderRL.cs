using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private IConfiguration config;
        public OrderRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public bool AddOrder(AddOrderModel addOrderModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                SqlCommand com = new SqlCommand("AddOrder", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@BookId", addOrderModel.BookId);
                com.Parameters.AddWithValue("@Id", addOrderModel.Id);
                com.Parameters.AddWithValue("@AddressId", addOrderModel.AddressId);
                com.Parameters.AddWithValue("@CartId", addOrderModel.CartId);

                com.Parameters.AddWithValue("@OrderDate", DateTime.Now);
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

        public bool DeleteOrder(int orderId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                SqlCommand com = new SqlCommand("sp_DeleteOrder", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@OrderId", orderId);


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

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_GetallOrders", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", userId);


                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                List<GetAllOrderModel> orders = new List<GetAllOrderModel>();
                while (reader.Read())
                {
                    var allorders = new GetAllOrderModel();
                    allorders.Id = reader.GetInt32(0);
                    allorders.OrderId = reader.GetInt32(1);
                    allorders.OrderDate = reader.GetDateTime(2);
                    allorders.TotalPrice = reader.GetInt32(3);

                    allorders.BookId = reader.GetInt32(4);
                    allorders.BookImage = reader.GetString(5);
                    allorders.BookName = reader.GetString(6);
                    allorders.DiscountedPrice = reader.GetDecimal(7);
                    allorders.CartId = reader.GetInt32(8);
                    allorders.Quantity = reader.GetInt32(9);
                  
                  
                    
                    allorders.AddressId = reader.GetInt32(10);
                    allorders.Address = reader.GetString(11);
                    allorders.City = reader.GetString(12);
                    allorders.State = reader.GetString(13);
                    allorders.TypeId = reader.GetInt32(14);
                    orders.Add(allorders);
                }

                connection.Close();
                return orders;


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

