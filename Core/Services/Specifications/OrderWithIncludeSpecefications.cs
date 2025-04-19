using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.OrderEntites;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecefications:Specfications<Order>
    {
        public OrderWithIncludeSpecefications(Guid id):base(f=>f.Id==id)
        {
            AddInclude(p => p.DeliveryMethod);
            AddInclude(l=>l.orderItems);
        }
        public OrderWithIncludeSpecefications(string Email) : base(f => f.UserEmail==Email)
        {
            AddInclude(p => p.DeliveryMethod);
            AddInclude(l => l.orderItems);
            SetOrderBy(g => g.OrderDate); 
        }

    }
}
