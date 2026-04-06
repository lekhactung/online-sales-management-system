using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
    public class Shipping
    {
        public string ShippingId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string? ShippingCompany { get; set; }
        public DateOnly? EstimatedDeliveryDate { get; set; }
        public decimal? ShippingFee { get; set; }
        public Order? Order { get; set; }

    }
}
