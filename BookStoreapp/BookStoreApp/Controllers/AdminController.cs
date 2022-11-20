using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        IAdminBL adminBL;
        private IConfiguration config;
        private readonly ILogger<AdminController> logger;
        public AdminController(IAdminBL adminBL, IConfiguration config, ILogger<AdminController> logger)
        {
            this.adminBL = adminBL;
            this.config = config;
            this.logger = logger;
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(AdminModel adminModel)
        {
            try
            {
                string token = this.adminBL.SignIn(adminModel);
                this.logger.LogInformation("  logged in successfully with email Id:" + adminModel.EmailId);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {adminModel.EmailId}" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
