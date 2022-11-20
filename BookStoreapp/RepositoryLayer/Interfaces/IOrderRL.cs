using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        bool AddOrder(AddOrderModel addOrderModel);
        bool DeleteOrder(int orderId);
        List<GetAllOrderModel> GetAllOrder(int userId);
    }
}
