using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.SignUp(userPostModel);

                return this.Ok(new { sucess = true, status = 200, message = $"Registration successful for {userPostModel.EmailId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(UserSignIn userSignIn)
        {
            try
            {
                string token = this.userBL.SignIn(userSignIn);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {userSignIn.EmailId}" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("ForgotPassword/{emailid}")]
        public IActionResult ForgotPassword(string emailid)
        {
            try
            {
                bool isExist = this.userBL.ForgotPassword(emailid);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to Email : {emailid}" });
                else return BadRequest(new { success = false, message = $"No user Exist with Email : {emailid}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword/{email}")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel,string email)
        {
            try
            {
                if (resetPasswordModel.NewPassword != resetPasswordModel.ConfirmNewPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
               
                bool res = this.userBL.ResetPassword(email, resetPasswordModel);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = $"Password not updated" });
                }
                return this.Ok(new { success = true, status = 200, message = "Password Changed Sucessfully" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
