using System;
using System.Globalization;

namespace AdvancedCSharp.Types.Task2
{
	/// <summary>
	/// A product in an Internet store.
	/// </summary>
	public struct Product : IEquatable<Product>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Product" /> class.
		/// </summary>
		/// <param name="name">Name of the new instance.</param>
		/// <param name="price">Price of the new instace.</param>
		/// <param name="quantity">Quality of the new instance.</param>
		/// <exception cref="ArgumentException">
		/// Throws when <paramref name="name" />, <paramref name="price" /> or
		/// <paramref name="quantity" /> is invalid.
		/// </exception>
		public Product(string name, decimal price, int quantity)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("The name must be not null or empry or contains white spaces", nameof(name));
			}

			if (price < 0)
			{
				throw new ArgumentException("The price must not be less than zero.", nameof(price));
			}

			if (quantity < 0)
			{
				throw new ArgumentException("The quantity must not be less than zero.", nameof(quantity));
			}

			Name = name;
			Price = price;
			Quantity = quantity;
		}

		/// <summary>
		/// Name of the product.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Price of the product.
		/// </summary>
		public decimal Price { get; }

		/// <summary>
		/// Количество товара.
		/// </summary>
		public int Quantity { get; }

		/// <summary>
		/// Implementation of the <see cref="IEquatable{T}.Equals(T)" />
		/// </summary>
		/// <param name="other">Object to equal compare.</param>
		/// <returns>Result of equal comparing.</returns>
		/// <remarks>
		/// Supposed, <see cref="Quantity" /> is not involved in equal comparing.
		/// </remarks>
		public bool Equals(Product other)
		{
			return Name == other.Name &&
					Price.Equals(other.Price);
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>
		/// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise,
		/// false.
		/// </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Product))
			{
				return false;
			}

			return Equals((Product) obj);
		}

		/// <summary>
		/// Overrided equality operator.
		/// </summary>
		/// <param name="left">The first object to equals comparing.</param>
		/// <param name="right">The second object to equlas comparing.</param>
		/// <returns>Results of the equals comparing.</returns>
		/// <remarks>
		/// Overrided in order to increase performace for struct.
		/// </remarks>
		public static bool operator ==(Product left, Product right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Overrided inequality operator.
		/// </summary>
		/// <param name="left">The first object to equals comparing.</param>
		/// <param name="right">The second object to equals comparing.</param>
		/// <returns>Results of the equality comparing.</returns>
		public static bool operator !=(Product left, Product right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode() => Name.GetHashCode() + Price.GetHashCode();

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
			=>
				string.Format(CultureInfo.CurrentCulture, "The product is: {0} - {1}, {2} - {3}, {4} - {5}",
					nameof(Name), Name, nameof(Price), Price.ToString("C", CultureInfo.CurrentCulture), nameof(Quantity),
					Quantity);
	}
}