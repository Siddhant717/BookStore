using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public void SignUp(UserPostModel userPostModel);
        public string SignIn(UserSignIn userSignIn);

        public bool ForgotPassword(string emailid);
        public bool ResetPassword(string email, ResetPasswordModel resetpasswordModel);
    }
}
