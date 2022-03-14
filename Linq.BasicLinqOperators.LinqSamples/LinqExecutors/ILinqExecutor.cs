namespace Task.LinqExecutors
{
	/// <summary>
	/// Executes a linq operation on <typeparamref name="TSource" /> and returns <typeparamref name="TResult" />.
	/// </summary>
	/// <typeparam name="TSource">Type of the source data.</typeparam>
	/// <typeparam name="TResult">Type of the return data.</typeparam>
	public interface ILinqExecutor<in TSource, out TResult>
	{
		/// <summary>
		/// Execute a linq operation.
		/// </summary>
		/// <param name="source">Source data to execute.</param>
		/// <returns>Result data.</returns>
		TResult Execute(TSource source);
	}
}