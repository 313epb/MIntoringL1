using System;
using System.Runtime.Serialization;
using Task.Database;

namespace Task.Serialization.SerializationSurrogates
{
	/// <summary>
	/// Implementes the <see cref="ISerializationSurrogate" /> to control serialization for the type <see cref="Order" />.
	/// </summary>
	public class OrderSerializationSurrogate : ISerializationSurrogate
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
			var order = obj as Order;

			if (order == null)
			{
				return;
			}

			info.AddValue(nameof(Order.OrderId), order.OrderId);
			info.AddValue(nameof(Order.CustomerId), order.CustomerId);
			info.AddValue(nameof(Order.EmployeeId), order.EmployeeId);
			info.AddValue(nameof(Order.OrderDate), order.OrderDate);
			info.AddValue(nameof(Order.RequiredDate), order.RequiredDate);
			info.AddValue(nameof(Order.ShippedDate), order.ShippedDate);
			info.AddValue(nameof(Order.ShipVia), order.ShipVia);
			info.AddValue(nameof(Order.Freight), order.Freight);
			info.AddValue(nameof(Order.ShipName), order.ShipName);
			info.AddValue(nameof(Order.ShipAddress), order.ShipAddress);
			info.AddValue(nameof(Order.ShipCity), order.ShipCity);
			info.AddValue(nameof(Order.ShipRegion), order.ShipRegion);
			info.AddValue(nameof(Order.ShipPostalCode), order.ShipPostalCode);
			info.AddValue(nameof(Order.ShipCountry), order.ShipCountry);
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
			var order = obj as Order;

			if (order == null)
			{
				return null;
			}

			order.OrderId = info.GetInt32(nameof(Order.OrderId));
			order.CustomerId = info.GetString(nameof(Order.CustomerId));
			order.EmployeeId = info.GetAs<int?>(nameof(Order.EmployeeId));
			order.OrderDate = info.GetAs<DateTime?>(nameof(Order.OrderDate));
			order.RequiredDate = info.GetAs<DateTime?>(nameof(Order.RequiredDate));
			order.ShippedDate = info.GetAs<DateTime?>(nameof(Order.ShippedDate));
			order.ShipVia = info.GetAs<int?>(nameof(Order.ShipVia));
			order.Freight = info.GetAs<decimal?>(nameof(Order.Freight));
			order.ShipName = info.GetString(nameof(Order.ShipName));
			order.ShipAddress = info.GetString(nameof(Order.ShipAddress));
			order.ShipCity = info.GetString(nameof(Order.ShipCity));
			order.ShipRegion = info.GetString(nameof(Order.ShipRegion));
			order.ShipPostalCode = info.GetString(nameof(Order.ShipPostalCode));
			order.ShipCountry = info.GetString(nameof(Order.ShipCountry));

			return order;
		}
	}
}