using System;
using System.Globalization;

namespace AdvancedCSharp.Types.Task1
{
	/// <summary>
	/// User info data.
	/// </summary>
	public struct InfoData : IEquatable<InfoData>
	{
		/// <summary>
		/// First name of the <see cref="InfoData" /> instance.
		/// </summary>
		public string FirstName { get; }

		/// <summary>
		/// Last name of the <see cref="InfoData" /> instance.
		/// </summary>
		public string LastName { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoData" /> structure.
		/// </summary>
		/// <param name="firstName">First name.</param>
		/// <param name="lastName">Last name.</param>
		/// <exception cref="ArgumentException">Throws when input parameters is invalid.</exception>
		public InfoData(string firstName, string lastName)
		{
			if (string.IsNullOrWhiteSpace(firstName))
			{
				throw new ArgumentException("The first name must not be empty or contains white spaces.");
			}

			if (string.IsNullOrWhiteSpace(lastName))
			{
				throw new ArgumentException("The last name must not be empty or contains white spaces.");
			}

			FirstName = firstName;
			LastName = lastName;
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
			if (!(obj is InfoData))
			{
				return false;
			}

			return Equals((InfoData) obj);
		}

		/// <summary>
		/// Overrided equality operator.
		/// </summary>
		/// <param name="left">The first object to equals comparing.</param>
		/// <param name="right">The second object to equlas comparing.</param>
		/// <returns>Results of the equals comparing.</returns>
		public static bool operator ==(InfoData left, InfoData right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Overrided inequality operator.
		/// </summary>
		/// <param name="left">The first object to equals comparing.</param>
		/// <param name="right">The second object to equlas comparing.</param>
		/// <returns>Results of the equals comparing.</returns>
		public static bool operator !=(InfoData left, InfoData right)
		{
			return !(left == right);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode() => FirstName.GetHashCode() + LastName.GetHashCode();

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
			=> string.Format(CultureInfo.CurrentCulture, "The user is: {0} {1}", FirstName, LastName);

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(InfoData other) => FirstName == other.FirstName && LastName == other.LastName;
	}
}