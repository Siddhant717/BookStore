using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private IConfiguration config;
        public AddressRL(IConfiguration Config)
        {

            this.config = Config;
        }
        public bool AddAddress(AddressModel addressModel, int userid)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_Add_Address", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Address", addressModel.Address);
                com.Parameters.AddWithValue("@City", addressModel.City);
                com.Parameters.AddWithValue("@State", addressModel.State);
                com.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                com.Parameters.AddWithValue("@Id", userid);
                

                connection.Open();
                var reader = com.ExecuteNonQuery();
                connection.Close();
                return true;

            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        

        public bool UpdateAddress(AddressModel addressModel, int AddressId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_UpdateAddress", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@AddressId", AddressId);
                com.Parameters.AddWithValue("@Address", addressModel.Address);
                com.Parameters.AddWithValue("@City", addressModel.City);
                com.Parameters.AddWithValue("@State", addressModel.State);
                com.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
               

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
        public bool DeleteAddress(int AddressId)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {

                SqlCommand com = new SqlCommand("sp_DeleteAddress", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@AddressId", AddressId);


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
