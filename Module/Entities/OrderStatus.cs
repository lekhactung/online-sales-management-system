using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
    public class OrderStatus
    {
        [Key]
        public string StatusId { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;

    }
}
