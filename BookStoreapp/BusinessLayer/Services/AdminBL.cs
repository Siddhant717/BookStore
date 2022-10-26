using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL: IAdminBL
    {
       readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public string SignIn(AdminModel adminModel)
        {
            try
            {
                return this.adminRL.SignIn(adminModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
