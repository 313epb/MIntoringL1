using System.Collections.Generic;
using System.Diagnostics;

namespace MvcMusicStore.Infrastructure
{
	/// <summary>
	/// Creates performance counters to work with the category LoggingAndMonitoring.
	/// </summary>
	public static class PerformanceCounterFactory
	{
		private const string CategoryName = "LoggingAndMonitoring";

		private const string LogInCounterName = "LogIn Successfull Count";

		private const string LogOffCounterName = "LogOff Successfull Count";

		private const string AlbumWatchedCounterName = "Album Watched Count";

		/// <summary>
		/// Creates a new instance <see cref="LogInCounterName" /> of the <see cref="PerformanceCounter" /> class.
		/// </summary>
		/// <returns>Created performance counter.</returns>
		public static PerformanceCounter CreateLogInCounter()
		{
			return new PerformanceCounter
			{
				CategoryName = CategoryName,
				CounterName = LogInCounterName,
				MachineName = ".",
				ReadOnly = false
			};
		}

		/// <summary>
		/// Creates a new instance <see cref="LogOffCounterName" /> of the <see cref="PerformanceCounter" /> class.
		/// </summary>
		/// <returns>Created performance counter.</returns>
		public static PerformanceCounter CreateLogOffCounter()
		{
			return new PerformanceCounter
			{
				CategoryName = CategoryName,
				CounterName = LogOffCounterName,
				MachineName = ".",
				ReadOnly = false
			};
		}

		/// <summary>
		/// Creates a new instance <see cref="AlbumWatchedCounterName" /> of the <see cref="PerformanceCounter" /> class.
		/// </summary>
		/// <returns>Created performance counter.</returns>
		public static PerformanceCounter CreateAlbumWatchedCounter()
		{
			return new PerformanceCounter
			{
				CategoryName = CategoryName,
				CounterName = AlbumWatchedCounterName,
				MachineName = ".",
				ReadOnly = false
			};
		}

		/// <summary>
		/// Creates instances of the <see cref="LogInCounterName"/>, <see cref="LogOffCounterName"/>, <see cref="AlbumWatchedCounterName"/> performance counters.
		/// </summary>
		/// <returns>Enumerable of the performance counters.</returns>
		public static IEnumerable<PerformanceCounter> CreatesCounters()
		{
			yield return CreateLogInCounter();
			yield return CreateLogOffCounter();
			yield return CreateAlbumWatchedCounter();
		}
	}
}