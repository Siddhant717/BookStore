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
    public class CartController : Controller
    {
       ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
       
        [Authorize(Roles = Role.Users)]
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartModel cartModel)
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                var result = this.cartBL.AddToCart(UserID, cartModel);
                if (result == true)
                {
                    return this.Ok(new { success = true, status = 200, message = "Book Added SuccessFully in Cart " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book is not added to cart"});
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.Users)]
        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(UpdateCartModel updatecartModel)
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                var result = this.cartBL.UpdateCart(updatecartModel);
                if (result == true)
                {
                    return this.Ok(new { success = true, status = 200, message = "Cart Updated Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart is not updated" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpDelete("DeleteCart/{CartId}")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
              
                var result = this.cartBL.DeleteCart(CartId);
                if (result == true)
                {
                    return this.Ok(new { success = true, status = 200, message = "Cart deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart is not deleted" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpGet("GetAllCarts")]
        public IActionResult GetAllCarts()
        {
            try
            {
                var result = this.cartBL.getAllCarts();
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
