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
    public class OrderController : Controller
    {
        IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public IActionResult AddBook(AddOrderModel addOrderModel)
        {
            try
            {
                var result = this.orderBL.AddOrder(addOrderModel);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Order placed successfully" });
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
        [HttpDelete("DeleteOrder/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                var result = this.orderBL.DeleteOrder(orderId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, status = 200, message = "Order deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { sucess = false, message = "Could not deleted" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Users)]
        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            try
            {
                var Userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(Userid.Value);
                var result = this.orderBL.GetAllOrder(UserID);
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
