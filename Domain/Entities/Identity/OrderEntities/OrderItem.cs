using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public ProductInOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
       public decimal SubTotal { get; set; }
    }
}
