using System;
using System.Runtime.Serialization;

namespace Task.Serialization
{
	/// <summary>
	/// Extension methods for <see cref="SerializationInfo" /> class.
	/// </summary>
	public static class SerializationInfoExtensions
	{
		/// <summary>
		/// Retrieves a value from the <see cref="SerializationInfo" /> class as <typeparamref name="T" /> class.
		/// </summary>
		/// <typeparam name="T">Type of retrived value.</typeparam>
		/// <param name="info">The <see cref="SerializationInfo" /> to populate the data.</param>
		/// <param name="name">The name is associated with the value to retrieve.</param>
		/// <returns>The retrieved value of the <typeparamref name="T" /> class.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		public static T GetAs<T>(this SerializationInfo info, string name)
		{
			if (info == null)
			{
				throw new ArgumentNullException(nameof(info));
			}

			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name must not be empty on contain white spaces.", nameof(name));
			}

			T value;

			try
			{
				value = (T) info.GetValue(name, typeof(T));
			}
			catch (InvalidCastException)
			{
				throw;
			}
			catch (Exception)
			{
				value = default(T);
			}

			return value;
		}
	}
}