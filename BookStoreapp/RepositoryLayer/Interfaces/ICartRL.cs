using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICartRL
    {
        bool AddToCart(int Id, CartModel cartModel);
        bool UpdateCart(UpdateCartModel updatecartModel);
        bool DeleteCart(int cartid);
        List<GetAllCartModel> getAllCarts();
    }
}
