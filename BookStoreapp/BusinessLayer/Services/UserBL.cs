using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

       public void SignUp(UserPostModel userPostModel)
        {
            try
            {
                this.userRL.SignUp(userPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SignIn(UserSignIn userSignIn)
        {
            try
            {
                return userRL.SignIn(userSignIn);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ForgotPassword(string emailid)
        {
            try
            {
                return this.userRL.ForgotPassword(emailid);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword(string email, ResetPasswordModel resetpasswordModel)
        {
            try
            {
                return this.userRL.ResetPassword(email, resetpasswordModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}