using System;
using System.Collections.Generic;
using System.Globalization;
using log4net;
using Newtonsoft.Json;

namespace AdvancedCSharp.Types.Task1.Processing
{
	/// <summary>
	/// Converts source data of type <typeparamref name="T" /> to JSON and writes into log.
	/// </summary>
	/// <typeparam name="T">Type of an object of sourca data.</typeparam>
	public class LogExporter<T> : IProcessor<T>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="LogExporter{T}" /> class.
		/// </summary>
		public LogExporter(ILog logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException(nameof(logger));
			}

			Logger = logger;
		}

		/// <summary>
		/// Logger.
		/// </summary>
		protected ILog Logger { get; }

		/// <summary>
		/// Executes process on source data of type <see cref="IEnumerable{T}" />.
		/// </summary>
		/// <param name="source">Source data to be processed.</param>
		public void Process(IEnumerable<T> source)
		{
			Logger.Info(string.Format(CultureInfo.CurrentCulture, "The user info data is: {0}", Format(source)));
		}

		/// <summary>
		/// Formats source data to preferred format.
		/// </summary>
		/// <param name="source">Source data to be formatted.</param>
		/// <returns>Formatted string representation of <paramref name="source" />.</returns>
		protected virtual string Format(IEnumerable<T> source)
		{
			return JsonConvert.SerializeObject(source);
		}
	}
}