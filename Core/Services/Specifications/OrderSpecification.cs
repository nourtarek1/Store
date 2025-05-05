using Domian.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecification : BaseSpecifications<Order,Guid>
    {
        public OrderSpecification(Guid id): base( O =>O.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);
        }

        public OrderSpecification(string userEmail) : base(O => O.UserEmail == userEmail)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);
            AddOrderBy(O => O.OrderDate);
        }
    }
}
