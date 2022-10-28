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
    public class WhishListController : Controller
    {
        IWhishListBL whishlistBL;
        public WhishListController(IWhishListBL whishlistBL)
        {
            this.whishlistBL = whishlistBL;
        }

        [Authorize(Roles = Role.Users)]
        [HttpPost("AddtoWhishlist")]
        public IActionResult AddtoWhishlist(WhishListModel whishlistModel)
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                var result = this.whishlistBL.AddtoWhishlist(whishlistModel,UserID);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Added to whishlist successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Not added to whishlist" });

                }
            }
            catch (Exception ex)
      
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Users)]
        [HttpDelete("DeleteFromWhishlist/{whishlistId}")]
        public IActionResult DeleteFromWhishlist(int whishlistId)
        {
            try
            {
                var result = this.whishlistBL.DeleteFromWhishlist(whishlistId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Book deleted successfully from whishlist" });
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
        [HttpGet("getAllWhishlist")]
        public IActionResult getAllWhishlist()
        {
            try
            {
                var result = this.whishlistBL.getAllWhishlist();
                if (result != null)
                {
                    return this.Ok(new { sucess = true, status = 200, data = result });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not fetched" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
