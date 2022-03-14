using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace ExceptionHandling.CodeContracts.CodeAuthorInfo
{
	/// <summary>
	/// Information about a code author.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class CodeAuthorInfoAttribute : Attribute, IEquatable<CodeAuthorInfoAttribute>,
		IComparable<CodeAuthorInfoAttribute>
	{
		private const string WhiteSpace = " ";

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeAuthorInfoAttribute" /> class.
		/// </summary>
		/// <param name="name">Author name.</param>
		/// <param name="email">Author email.</param>
		public CodeAuthorInfoAttribute(string name, string email)
		{
			Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name),
				"The name mustn't be null or contain white spaces.");
			Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(email),
				"The email mustn't be null or contain white spaces.");

			Name = name;
			Email = email;
		}

		/// <summary>
		/// Author name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Author email.
		/// </summary>
		public string Email { get; }

		/// <summary>Compares the current object with another object of the same type.</summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following
		/// meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This
		/// object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(CodeAuthorInfoAttribute other)
		{
			if (other == null)
			{
				return 1;
			}

			return string.CompareOrdinal(Name, other.Name) + string.CompareOrdinal(Email, other.Email);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(CodeAuthorInfoAttribute other)
		{
			if (other == null)
			{
				return false;
			}

			return
				Name.Replace(WhiteSpace, string.Empty)
					.Equals(other.Name.Replace(WhiteSpace, string.Empty), StringComparison.OrdinalIgnoreCase) &&
				Email.Replace(WhiteSpace, string.Empty)
					.Equals(other.Email.Replace(WhiteSpace, string.Empty), StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Overrided greater than operator.
		/// </summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>Results of comparing.</returns>
		public static int operator >(CodeAuthorInfoAttribute left, CodeAuthorInfoAttribute right)
		{
			if (left == null && right == null)
			{
				return 0;
			}

			if (left == null)
			{
				return -1;
			}

			return left.CompareTo(right);
		}

		/// <summary>
		/// Overrided less than operator.
		/// </summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>Results of comparing.</returns>
		public static int operator <(CodeAuthorInfoAttribute left, CodeAuthorInfoAttribute right)
		{
			if (left == null && right == null)
			{
				return 0;
			}

			if (left == null)
			{
				return -1;
			}

			return left.CompareTo(right);
		}

		/// <summary>
		/// Overrided equality operator.
		/// </summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>Results of equality comparing.</returns>
		public static bool operator ==(CodeAuthorInfoAttribute left, CodeAuthorInfoAttribute right)
		{
			return Equals(left, right);
		}

		/// <summary>
		/// Overrided inequality operator.
		/// </summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>Results of equality comparing.</returns>
		public static bool operator !=(CodeAuthorInfoAttribute left, CodeAuthorInfoAttribute right)
		{
			return left != right;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			var codeAuthorInfoAttribute = obj as CodeAuthorInfoAttribute;

			return codeAuthorInfoAttribute != null && Equals(codeAuthorInfoAttribute);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return Name.GetHashCode() + Email.GetHashCode();
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "The code author is: {0} {1}", Name, Email);
		}
	}
}