using System;
using System.Diagnostics;

namespace AdvancedCSharp.Types.Task1.PerformanceMeasure
{
	/// <summary>
	/// Executes performance measures.
	/// </summary>
	public class PerformanceMeasureExecutor : IDisposable
	{
		private Stopwatch _stopwatch;

		/// <summary>
		/// Initializes a new instance of the <see cref="PerformanceMeasureExecutor" /> class and starts measure immediately.
		/// </summary>
		public PerformanceMeasureExecutor()
		{
			Start();
		}

		/// <summary>
		/// Performance measure info.
		/// </summary>
		public PerformanceMeasureInfo PerformanceMeasureInfo { get; private set; }

		/// <summary>
		/// Completes performance measure and calculates results.
		/// </summary>
		public void Dispose()
		{
			Complete();
		}

		private void Start()
		{
			_stopwatch = Stopwatch.StartNew();

			PerformanceMeasureInfo = new PerformanceMeasureInfo
			{
				StartDataTime = DateTime.Now,
				StartAllocatedMemory = GC.GetTotalMemory(false)
			};
		}

		private void Complete()
		{
			_stopwatch.Stop();

			PerformanceMeasureInfo.CompleteDateTime = DateTime.Now;
			PerformanceMeasureInfo.CompleteAllocatedMemory = GC.GetTotalMemory(false);
			PerformanceMeasureInfo.Elapsed = _stopwatch.Elapsed;
		}
	}
}