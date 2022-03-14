using System.Collections.Generic;

namespace AdvancedCSharp.Types.Task1.Processing
{
	/// <summary>
	/// Allows to process source data of type <typeparamref name="T" />.
	/// </summary>
	/// <typeparam name="T">Type of an object of source data to be processed.</typeparam>
	public interface IProcessor<in T>
	{
		/// <summary>
		/// Executes process on source data of type <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <param name="source">Source data to be processed.</param>
		void Process(IEnumerable<T> source);
	}
}