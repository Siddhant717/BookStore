using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
       readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public bool AddOrder(AddOrderModel addOrderModel)
        {
            try
            {
                return this.orderRL.AddOrder(addOrderModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            try
            {
                return this.orderRL.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.orderRL.GetAllOrder(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
