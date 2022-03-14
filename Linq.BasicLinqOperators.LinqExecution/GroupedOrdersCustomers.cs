using System.Collections.Generic;
using System.Linq;

namespace Task.Data
{
    public class GroupedOrdersCustomers<TK>
    {
        public Customer Customer { get; set; }
        public IEnumerable<IGrouping<TK, Order>> GroupedOrders { get; set; }
    }
}