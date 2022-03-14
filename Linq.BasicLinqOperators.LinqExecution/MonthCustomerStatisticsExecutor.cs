using System;
using System.Collections.Generic;
using System.Linq;
using Task.Data;

namespace Task.LinqExecutors
{
    public class MonthCustomerStatisticsExecutor :
        ILinqExecutor<IEnumerable<Customer>, IEnumerable<GroupedOrdersCustomers<int>>>
    {
        public IEnumerable<GroupedOrdersCustomers<int>> Execute(IEnumerable<Customer> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return from customer in source
                select new GroupedOrdersCustomers<int>
                {
                    Customer = new Customer
                    {
                        Address = customer.Address,
                        City = customer.City,
                        CompanyName = customer.CompanyName,
                        Country = customer.Country,
                        CustomerId = customer.CustomerId,
                        Fax = customer.Fax,
                        Phone = customer.Phone,
                        PostalCode = customer.PostalCode,
                        Region = customer.Region
                    },
                    GroupedOrders = from order in customer.Orders
                        group order by order.OrderDate.Month
                        into groupedOrders
                        orderby groupedOrders.Key
                        select groupedOrders
                };
        }
    }
}