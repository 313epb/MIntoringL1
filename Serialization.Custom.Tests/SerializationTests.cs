using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Database;
using Task.Serialization.DataCotractSurrogates;
using Task.Serialization.SerializationSurrogates;
using Task.TestHelpers;

namespace Task
{
	[TestClass]
	public class SerializationTests
	{
		private NorthwindContext _northwindContext;

		[TestInitialize]
		public void Initialize()
		{
			_northwindContext = new NorthwindContext();
		}

		/// <summary>
		/// Extends serialization with callbacks technique.
		/// </summary>
		/// <remarks>
		/// Implementation details:
		/// Implemented <see cref="OnSerializingAttribute" /> handler in the <see cref="Category" /> class.
		/// </remarks>
		[TestMethod]
		public void SerializationCallbacks()
		{
			_northwindContext.Configuration.ProxyCreationEnabled = false;

			var streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain, _northwindContext);
			var tester = new DataContractSerializerTester<IEnumerable<Category>>(
				new NetDataContractSerializer(streamingContext), true);
			var categories = _northwindContext.Categories.ToList();

			tester.SerializeAndDeserialize(categories);
		}

		/// <summary>
		/// Extends serialization with the <see cref="ISerializable" /> interface.
		/// </summary>
		/// <remarks>
		/// Implementation details:
		/// Implemented <see cref="ISerializable" /> on <see cref="Product" />, <see cref="Category" />, <see cref="Supplier" />,
		/// <see cref="OrderDetail" />.
		/// </remarks>
		[TestMethod]
		public void Serializable()
		{
			_northwindContext.Configuration.ProxyCreationEnabled = false;

			var streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain, _northwindContext);
			var tester = new DataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(streamingContext),
				true);
			var products = _northwindContext.Products.ToList();

			tester.SerializeAndDeserialize(products);
		}

		/// <summary>
		/// Extends serialization with the <see cref="ISerializationSurrogate" /> interface.
		/// </summary>
		/// <remarks>
		/// Implementation details:
		/// Implemented the <see cref="ISerializationSurrogate" /> for types <see cref="OrderDetail" />, <see cref="Order" />
		/// </remarks>
		[TestMethod]
		public void SerializationSurrogate()
		{
			_northwindContext.Configuration.ProxyCreationEnabled = false;

			var surrogateSelector = new SurrogateSelector();
			var streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain, _northwindContext);

			surrogateSelector.AddSurrogate(typeof(OrderDetail), streamingContext, new OrderDetailSerializationSurrogate());
			surrogateSelector.AddSurrogate(typeof(Order), streamingContext, new OrderSerializationSurrogate());
			surrogateSelector.AddSurrogate(typeof(Product), streamingContext, new ProductSerializationSurrogate());

			var netDataContractSerializer = new NetDataContractSerializer(streamingContext)
			{
				SurrogateSelector = surrogateSelector
			};

			var tester = new DataContractSerializerTester<IEnumerable<OrderDetail>>(netDataContractSerializer, true);
			var orderDetails = _northwindContext.OrderDetails.ToList();

			tester.SerializeAndDeserialize(orderDetails);
		}

		/// <summary>
		/// Extends serialization with the <see cref="IDataContractSurrogate" /> interface.
		/// </summary>
		/// <remarks>
		/// Implementation details:
		/// Implemented the <see cref="IDataContractSurrogate" /> interface for collection of the <see cref="Order" /> class.
		/// </remarks>
		[TestMethod]
		public void DataContractSurrogate()
		{
			_northwindContext.Configuration.ProxyCreationEnabled = true;
			_northwindContext.Configuration.LazyLoadingEnabled = true;

			var tester =
				new DataContractSerializerTester<IEnumerable<Order>>(
					new DataContractSerializer(typeof(IEnumerable<Order>), new List<Type>(), int.MaxValue,
						false, true,
						new OrderCollectionDataContractSurrogate()), true);
			var orders = _northwindContext.Orders.ToList();

			tester.SerializeAndDeserialize(orders);
		}
	}
}