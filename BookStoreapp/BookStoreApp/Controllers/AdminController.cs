using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        IAdminBL adminBL;
        private IConfiguration _config;
        public AdminController(IAdminBL adminBL, IConfiguration config)
        {
            this.adminBL = adminBL;
            this._config = config;
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(AdminModel adminModel)
        {
            try
            {
                string token = this.adminBL.SignIn(adminModel);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {adminModel.EmailId}" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
