using Domain.Contracts;
using Domain.Entities.Identity.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecification (string paymentIntentId): Specification<Order> (x=>x.PaymentIntentId == paymentIntentId)
    {

    }
}
