using System;
using System.Globalization;

namespace AdvancedCSharp.Types.Task1.PerformanceMeasure
{
	/// <summary>
	/// Stores information about performance measure.
	/// </summary>
	public class PerformanceMeasureInfo
	{
		/// <summary>
		/// Date and time of start measure.
		/// </summary>
		public DateTime StartDataTime { get; set; }

		/// <summary>
		/// Complete date and time of measure.
		/// </summary>
		public DateTime CompleteDateTime { get; set; }

		/// <summary>
		/// Elapsed timespan of execution.
		/// </summary>
		public TimeSpan Elapsed { get; set; }

		/// <summary>
		/// Allocated memory before start measure.
		/// </summary>
		public long StartAllocatedMemory { get; set; }

		/// <summary>
		/// Allocated memory after complete measure.
		/// </summary>
		public long CompleteAllocatedMemory { get; set; }

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
			=>
				string.Format(CultureInfo.CurrentCulture,
					"Performance measure info: {0} - {1}, {2} - {3}, {4} - {5}, {6} - {7}, {8} - {9}",
					nameof(StartDataTime), StartDataTime,
					nameof(CompleteDateTime), CompleteDateTime,
					nameof(Elapsed), Elapsed,
					nameof(StartAllocatedMemory), StartAllocatedMemory,
					nameof(CompleteAllocatedMemory), CompleteAllocatedMemory);
	}
}