using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL:ICartBL
    {
        readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddToCart(int Id, CartModel cartModel)
        {
            try
            {
                return this.cartRL.AddToCart(Id,cartModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public bool UpdateCart(UpdateCartModel updatecartModel)
        {
            try
            {
                return this.cartRL.UpdateCart(updatecartModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCart(int cartid)
        {
            try
            {
                return this.cartRL.DeleteCart(cartid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAllCartModel> getAllCarts()
        {
            try
            {
                return this.cartRL.getAllCarts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
