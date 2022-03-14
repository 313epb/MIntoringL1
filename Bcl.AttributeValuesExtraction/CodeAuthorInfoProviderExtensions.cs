using System;
using System.Collections.Generic;

namespace Bcl.AttributeValuesExtraction
{
	/// <summary>
	/// Extensions for the <see cref="CodeAuthorInfoProvider" /> class.
	/// </summary>
	public static class CodeAuthorInfoProviderExtensions
	{
		/// <summary>
		/// Returns distinct elements from the <paramref name="source" />.
		/// </summary>
		/// <param name="source">Source sequence of information.</param>
		/// <returns>Sequence of unique elements.</returns>
		public static IEnumerable<CodeAuthorInfoAttribute> DistinctInfo(this IEnumerable<CodeAuthorInfoAttribute> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			var result = new List<CodeAuthorInfoAttribute>();

			foreach (var element in source)
			{
				if (result.ContainsInfo(element))
				{
					continue;
				}

				result.Add(element);
			}

			return result;
		}

		private static bool ContainsInfo(this IEnumerable<CodeAuthorInfoAttribute> source, CodeAuthorInfoAttribute item)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			foreach (var element in source)
			{
				if (element.Equals(item))
				{
					return true;
				}
			}

			return false;
		}
	}
}