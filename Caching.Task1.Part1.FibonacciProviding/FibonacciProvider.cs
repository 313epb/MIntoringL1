using Caching.Task1.FibonacciGenerating;
using Caching.Task1.FibonacciProviding.Cache;
using System;
using System.Globalization;

namespace Caching.Task1.FibonacciProviding
{
	/// <summary>
	/// Provides Fibbonacci sequences stored in cache.
	/// </summary>
	public class FibonacciProvider
	{
		/// <summary>
		/// Key format of a cache entry for a Fibbonacci sequence.
		/// </summary>
		public const string FibonacciKeyFormat = "Fibbonacchi.Length={0}";

		private readonly ICacheManager _cacheManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="FibonacciProvider" /> class.
		/// </summary>
		public FibonacciProvider(ICacheManager cacheManager)
		{
			if (cacheManager == null)
			{
				throw new ArgumentNullException(nameof(cacheManager));
			}

			_cacheManager = cacheManager;
		}

		/// <summary>
		/// Gets a Fibbonacci sequence, if there is no sequence with the specified <paramref name="length" /> in the cache,
		/// a sequence will be generated and saved to the cache, otherwise, requested sequence will be returned from the cache.
		/// </summary>
		/// <param name="length">The length of a sequence.</param>
		/// <returns>Fibbonacci sequence.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int[] GetSequence(int length)
		{
			string key = string.Format(CultureInfo.CurrentCulture, FibonacciKeyFormat, length);

			if (_cacheManager.Contains(key))
			{
				return _cacheManager.GetEntry<int[]>(key);
			}

			var sequence = FibonacciGenerator.Sequence(length);

			_cacheManager.SetEntry(key, sequence);

			return sequence;
		}
	}
}