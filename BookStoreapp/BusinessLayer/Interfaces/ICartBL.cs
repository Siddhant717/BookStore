using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICartBL
    {
        bool AddToCart(int Id,CartModel cartModel);
        bool UpdateCart(UpdateCartModel updatecartModel);

        bool DeleteCart(int cartid);
        List<GetAllCartModel> getAllCarts();
    }
}
