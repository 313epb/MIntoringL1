using System.Linq;
using System.Runtime.Serialization;
using Task.Database;

namespace Task.Serialization.SerializationSurrogates
{
	/// <summary>
	/// Implementes the <see cref="ISerializationSurrogate" /> to control serialization for the type <see cref="OrderDetail" />
	/// .
	/// </summary>
	public class OrderDetailSerializationSurrogate : ISerializationSurrogate
	{
		/// <summary>
		/// Populates the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to
		/// serialize the object.
		/// </summary>
		/// <param name="obj">The object to serialize. </param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">
		/// The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this
		/// serialization.
		/// </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var orderDetail = obj as OrderDetail;

			if (orderDetail == null)
			{
				return;
			}

			info.AddValue(nameof(OrderDetail.OrderId), orderDetail.OrderId);
			info.AddValue(nameof(OrderDetail.ProductId), orderDetail.ProductId);
			info.AddValue(nameof(OrderDetail.UnitPrice), orderDetail.UnitPrice);
			info.AddValue(nameof(OrderDetail.Quantity), orderDetail.Quantity);
			info.AddValue(nameof(OrderDetail.Discount), orderDetail.Discount);

			var northwindContext = context.Context as NorthwindContext;

			if (northwindContext == null)
			{
				return;
			}

			if (orderDetail.Product == null)
			{
				orderDetail.Product = northwindContext.Products.SingleOrDefault(p => p.ProductId == orderDetail.ProductId);
			}

			if (orderDetail.Order == null)
			{
				orderDetail.Order = northwindContext.Orders.SingleOrDefault(o => o.OrderId == orderDetail.OrderId);
			}

			info.AddValue(nameof(OrderDetail.Product), orderDetail.Product);
			info.AddValue(nameof(OrderDetail.Order), orderDetail.Order);
		}

		/// <summary>
		/// Populates the object using the information in the
		/// <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
		/// </summary>
		/// <returns>The populated deserialized object.</returns>
		/// <param name="obj">The object to populate. </param>
		/// <param name="info">The information to populate the object. </param>
		/// <param name="context">The source from which the object is deserialized. </param>
		/// <param name="selector">The surrogate selector where the search for a compatible surrogate begins. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			var orderDetail = obj as OrderDetail;

			if (orderDetail == null)
			{
				return null;
			}

			orderDetail.OrderId = info.GetInt32(nameof(OrderDetail.OrderId));
			orderDetail.ProductId = info.GetInt32(nameof(OrderDetail.ProductId));
			orderDetail.UnitPrice = info.GetDecimal(nameof(OrderDetail.UnitPrice));
			orderDetail.Quantity = info.GetInt16(nameof(OrderDetail.Quantity));
			orderDetail.Discount = info.GetSingle(nameof(OrderDetail.Discount));
			orderDetail.Order = info.GetValue(nameof(OrderDetail.Order), typeof(Order)) as Order;
			orderDetail.Product = info.GetValue(nameof(OrderDetail.Product), typeof(Product)) as Product;

			return orderDetail;
		}
	}
}