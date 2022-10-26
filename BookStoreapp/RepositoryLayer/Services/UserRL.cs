using CommonLayer.Model;
using Experimental.System.Messaging;
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
    public class UserRL : IUserRL
    {
       
        private IConfiguration config;
        public UserRL(IConfiguration Config)
        {

            this.config = Config;
        }

      public void SignUp(UserPostModel userPostModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                
                SqlCommand com = new SqlCommand("sp_SignUp", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@FullName", userPostModel.FullName);
                com.Parameters.AddWithValue("@EmailId", userPostModel.EmailId);
                com.Parameters.AddWithValue("@Password", userPostModel.Password);
                com.Parameters.AddWithValue("@MobileNumber", userPostModel.MobileNumber);

                connection.Open();
                var reader = com.ExecuteNonQuery();
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
        public string SignIn(UserSignIn userSignIn)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                SqlCommand com = new SqlCommand("sp_SignIn", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EmailId", userSignIn.EmailId);
                com.Parameters.AddWithValue("@Password", userSignIn.Password);

                connection.Open();
                var result = com.ExecuteScalar();
                if (result != null)
                {
                    var sql = "select Id from Users_Details where EmaiLId =@EmailId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@EmailId", userSignIn.EmailId);
                    var Id = cmd.ExecuteScalar();
                    var ID = Convert.ToInt32(Id);
                    return GenerateJwtToken(userSignIn.EmailId, ID);
                }
                else
                {
                    return null;
                }
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

        public bool ForgotPassword(string emailid)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                SqlCommand com = new SqlCommand("sp_ForgotPassword", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@EmailId", emailid);

                connection.Open();
                var result = com.ExecuteScalar();
                if (result != null)
                {
                    var sql = "select Id from Users_Details where EmaiLId =@EmailId";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@EmailId", emailid);
                    var Id = cmd.ExecuteScalar();
                    var ID = Convert.ToInt32(Id);


                    MessageQueue BookstoreQ = new MessageQueue();

                    //Setting the QueuPath where we want to store the messages.
                    BookstoreQ.Path = @".\private$\BookStore";
                    if (MessageQueue.Exists(BookstoreQ.Path))
                    {

                        BookstoreQ = new MessageQueue(@".\private$\BookStore");
                        //Exists
                    }
                    else
                    {
                        // Creates the new queue 
                        MessageQueue.Create(BookstoreQ.Path);
                    }
                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJwtToken(emailid, ID);
                    MyMessage.Label = "Forget Password Email";
                    BookstoreQ.Send(MyMessage);
                    Message msg = BookstoreQ.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(emailid, msg.Body.ToString());
                    BookstoreQ.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    BookstoreQ.BeginReceive();
                    BookstoreQ.Close();

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)

            {

                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)

                {

                    Console.WriteLine("Access is denied. " +

                        "Queue might be a system queue.");

                }

            }
        }

        private string GenerateToken(string emailid)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("EmailId", emailid),

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

        public bool ResetPassword(string email, ResetPasswordModel resetpasswordModel)
        {
            SqlConnection connection = new SqlConnection(config["ConnectionStrings:BookStore"]);
            try
            {
                SqlCommand com = new SqlCommand("sp_ResetPassword", connection);
                com.CommandType = System.Data.CommandType.StoredProcedure;

               if(resetpasswordModel.NewPassword!=resetpasswordModel.ConfirmNewPassword)
                {
                    return false;
                }
                com.Parameters.AddWithValue("@EmailId", email);
                com.Parameters.AddWithValue("@Password", resetpasswordModel.NewPassword);
               

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



