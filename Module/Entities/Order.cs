using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string StatusId { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public OrderStatus? Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public Shipping? Shipping { get; set; }

    }
}
