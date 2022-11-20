using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        bool AddOrder(AddOrderModel addOrderModel);
        bool DeleteOrder(int orderId);
        List<GetAllOrderModel> GetAllOrder(int userId);
    }
}
