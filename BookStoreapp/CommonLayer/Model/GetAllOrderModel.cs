using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class GetAllOrderModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int BookId { get; set; }
        public string BookImage { get; set; }
        public string BookName { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int TypeId { get; set; }
    }
}

