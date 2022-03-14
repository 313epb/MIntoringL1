namespace Task.Data
{
	/// <summary>
	/// A month-year key used in grouping LINQ operations.
	/// </summary>
	public struct MonthYearKey
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MonthYearKey" /> structure.
		/// </summary>
		/// <param name="month">A month.</param>
		/// <param name="year">A year.</param>
		public MonthYearKey(int month, int year)
		{
			Month = month;
			Year = year;
		}

		/// <summary>
		/// A month.
		/// </summary>
		public int Month { get; set; }

		/// <summary>
		/// A year.
		/// </summary>
		public int Year { get; set; }
	}
}