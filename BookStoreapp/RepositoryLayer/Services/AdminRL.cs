using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
      private IConfiguration config;
        public AdminRL(IConfiguration Config)
        {

            this.config = Config;
        }

        public string SignIn(AdminModel adminModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
               
                SqlCommand com = new SqlCommand("sp_AdminSignIn", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EmailId", adminModel.EmailId);
                com.Parameters.AddWithValue("@Password", adminModel.Password);

                connection.Open();
                var result = com.ExecuteScalar();
                if (result != null)
                {
                    var sql = "select Id from Admin_Details where EmaiLId =@EmailId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@EmailId", adminModel.EmailId);
                    var Id = cmd.ExecuteScalar();
                    var ID = Convert.ToInt32(Id);
                    return GenerateJwtToken(adminModel.EmailId, ID);
                }
                else
                {
                    return null;
                }


            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        private string GenerateJwtToken(string emailId, int userId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", emailId),
                    new Claim("userId",userId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
    
}