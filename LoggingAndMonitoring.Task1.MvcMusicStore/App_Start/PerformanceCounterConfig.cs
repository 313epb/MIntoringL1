using MvcMusicStore.Infrastructure;
using NLog;

namespace MvcMusicStore
{
	public class PerformanceCounterConfig
	{
		private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Clears all performance counters.
		/// </summary>
		public static void ClearCounters()
		{
			Logger.Debug("Start to initialize performance counters.");

			foreach (var performanceCounter in PerformanceCounterFactory.CreatesCounters())
			{
				performanceCounter.RawValue = 0;
			}

			Logger.Info("The performance counters has initialized.");
		}
	}
}