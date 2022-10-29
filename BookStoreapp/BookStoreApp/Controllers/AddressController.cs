using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }


        [Authorize(Roles = Role.Users)]
        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                var result = this.addressBL.AddAddress(addressModel, UserID);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Address Added Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Failed" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpPut("UpdateAddress/{AddressId}")]
        public IActionResult UpdateAddress(AddressModel addressModel,int AddressId)
        {
            try
            {
                var result = this.addressBL.UpdateAddress(addressModel, AddressId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Address Updated Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Failed" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpDelete("DeleteAddress/{AddressId}")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                var result = this.addressBL.DeleteAddress(AddressId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Address deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Failed" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
