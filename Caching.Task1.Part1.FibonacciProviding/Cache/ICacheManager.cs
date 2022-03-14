namespace Caching.Task1.FibonacciProviding.Cache
{
	/// <summary>
	/// Defines methods to communicate with cache.
	/// </summary>
	public interface ICacheManager
	{
		/// <summary>
		/// Sets <paramref name="value" /> to cache.
		/// </summary>
		/// <param name="key">Key of the saved cache entry.</param>
		/// <param name="value">Value to be saved.</param>
		/// <typeparam name="TValue">Type of the <paramref name="value" />.</typeparam>
		void SetEntry<TValue>(string key, TValue value);

		/// <summary>
		/// Gets a cache entry by the <paramref name="key" />.
		/// </summary>
		/// <param name="key">Key of the retrieving cache entry.</param>
		/// <typeparam name="TValue">Type of the retrieving cache entry.</typeparam>
		/// <returns>Retrieved cache value.</returns>
		TValue GetEntry<TValue>(string key);

		/// <summary>
		/// Determines whether cache contains an entry with the specified <paramref name="key" /> or not.
		/// </summary>
		/// <param name="key">Key of an cache entry.</param>
		/// <returns>An cache entiry exists in cache or not.</returns>
		bool Contains(string key);
	}
}